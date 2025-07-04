using System;
using System.Text.Json;
using System.Threading.Tasks;
using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Extensions;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Data;
using ArgentumOnline.Net;
using ArgentumOnline.UI.Gameplay;
using Godot;

namespace ArgentumOnline.UI;

/// <summary>
/// Manages the user interface for the game, including inventory interactions, trade and bank panels,  console output,
/// and handling player input events.
/// </summary>
/// <remarks>This class provides functionality for managing various aspects of the game's user interface, such as 
/// displaying inventory, handling input events, and managing trade and bank interactions. It integrates  with the
/// game's context to ensure proper synchronization of player actions and game state.  The class is designed to handle
/// input events like mouse clicks and key presses, and it provides methods  for opening and closing trade and bank
/// panels, writing messages to the console, and interacting with  inventory items.  Note that this class assumes the
/// presence of a valid <see cref="GameContext"/> instance to function  correctly.</remarks>
public partial class GameUIController : CanvasLayer
{
    #region Exported Properties
    [Export] private PackedScene TradePanelDisplayScene { get; set; }
    [Export] private PackedScene BankPanelDisplayScene { get; set; }
    
    [Export] private Camera2D MainCamera { get; set; }
    [Export] private RichTextLabel ConsoleOutput { get; set; }
    [Export] private InventoryContainerDisplay InventoryContainer { get; set; }
    #endregion

    private Node _currentPanelDisplay;
    
    public GameContext GameContext { get; set; }
    
    public void Initialize()
    {
        InventoryContainer.SetInventory(GameContext.PlayerInventory);
        InventoryContainer.SlotPressed += OnInventoryContainerSlotPressed;
    }
    
    public override void _ExitTree()
    {
        InventoryContainer.SlotPressed -= OnInventoryContainerSlotPressed;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton button)
        {
            HandleEventMouseButton(button);
        }

        if (@event is InputEventKey eventKey)
        {
            HandleEventKey(eventKey);
        }
    }

    private void HandleEventKey(InputEventKey eventKey)
    {
        if (eventKey.IsActionPressed("use_object"))
        {
            UseInventoryItem(false);
        }

        if (eventKey.IsActionPressed("attack"))
        {
            NetworkClient.Instance.SendAttack();
        }

        if (eventKey.IsActionPressed("pickup"))
        {
            PickupItem();
        }

        if (eventKey.IsActionPressed("toggle_safe_mode"))
        {
            NetworkClient.Instance.SendSafeMode();
        }
        
        if (eventKey.IsActionPressed("toggle_resuscitation_safe"))
        {
            NetworkClient.Instance.SendResuscitationSafe();
        }

        if (eventKey.IsActionPressed("equip_item"))
        {
            EquipSelectedItem(); 
        }

        if(eventKey.IsActionPressed("meditate"))
        {
            Meditate();
        }

        if (eventKey.IsActionPressed("take_screenshot"))
        {
            TakeScreenshot();
        }
        
        if(eventKey.IsActionPressed("position_update"))
        {
            if(GameContext.Intervals.RequestPositionUpdate())
            {
                NetworkClient.Instance.SendRequestPositionUpdate();
            }
        }
    }

    private void Meditate()
    {
        if(!GameContext.PlayerStats.IsAlive)
        {
            WriteToConsole("¡¡Estás muerto!!", GameAssets.FontDataList[(int)FontTypeNames.FontType_Info]);
            return;
        }

        if(GameContext.PlayerStats.MinMp != GameContext.PlayerStats.MaxMp)
        {
            NetworkClient.Instance.SendMeditate();
        }
    }

    private void TakeScreenshot()
    {
        try
        {
            Viewport viewport = GetViewport();
            Image image = viewport.GetTexture().GetImage();
            
            string now = DateTime.Now.Ticks.ToString();

            string path = $"user://screenshots/{now}.png";
            
            string absolutePath = ProjectSettings.GlobalizePath(path);

            var metaData = new
            {
                type = "screenshot",
                path = absolutePath,
            };

            string meta = JsonSerializer.Serialize(metaData);

            if (image.SavePng(path) == Error.Ok)
            {
                WriteToConsole($"[url={meta}]¡Screen Capturada![/url]",
                    new FontData(Color: Colors.White, false, false));
            }
        }
        catch (Exception e)
        {
            GD.PrintErr($"TakeScreenshot: {e.Message}");
        }
    }

    private void PickupItem()
    {
        if (!GameContext.PlayerStats.IsAlive)
        {
            WriteToConsole("¡¡Estás muerto!!", GameAssets.FontDataList[(int)FontTypeNames.FontType_Info]);
            return;
        }
        NetworkClient.Instance.SendPickup();
    }

    private void EquipSelectedItem()
    {
        if (!GameContext.PlayerStats.IsAlive)
        {
            WriteToConsole("¡¡Estás muerto!!", GameAssets.FontDataList[(int)FontTypeNames.FontType_Info]);
            return;
        }

        if (InventoryContainer.SelectedSlot != Declares.InvalidSlot)
        {
            NetworkClient.Instance.SendEquipItem(InventoryContainer.SelectedSlot + 1);
        }
    }

    private void HandleEventMouseButton(InputEventMouseButton mouseEvent)
    {
        Vector2 mouseViewPosition= MainCamera.GetCanvasTransform().AffineInverse() * mouseEvent.Position;
        Vector2I mouseTilePosition = (mouseViewPosition / 32.0f).Ceil().ToVector2I();

        if (GameContext.Trading)
        {
            return;
        }

        if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (mouseEvent.DoubleClick)
            {
                NetworkClient.Instance.SendDoubleClick(mouseTilePosition.X, mouseTilePosition.Y);
                return;
            }

            if (GameContext.UsingSkill == Skill.None)
            {
                NetworkClient.Instance.SendLeftClick(mouseTilePosition.X, mouseTilePosition.Y);
            }
            else
            {
                if (GameContext.UsingSkill == Skill.Proyectiles &&
                    !GameContext.Intervals.RequestAttackWithBow())
                {
                    GameContext.UsingSkill = Skill.None;
                    RestoreDefaultCursor();
                    
                    WriteToConsole("No puedes lanzar proyectiles tan rápido.",
                        GameAssets.FontDataList[(int)FontTypeNames.FontType_Talk]);
                    return;
                }

                if (GameContext.UsingSkill == Skill.Magia &&
                    !GameContext.Intervals.RequestCastSpell())
                {
                    GameContext.UsingSkill = Skill.None;
                    RestoreDefaultCursor();
                    
                    WriteToConsole("No puedes lanzar hechizos tan rápido.",
                        GameAssets.FontDataList[(int)FontTypeNames.FontType_Talk]);
                    return;
                }

                if (GameContext.UsingSkill is Skill.Mineria or Skill.Robar or Skill.Pesca or Skill.Talar 
                    or Skill.FundirMetal && !GameContext.Intervals.RequestWork())
                {
                    GameContext.UsingSkill = Skill.None;
                    RestoreDefaultCursor();
                    return;
                }
                
                NetworkClient.Instance.SendWorkLeftClick(mouseTilePosition.X, mouseTilePosition.Y, (int)GameContext.UsingSkill);

                GameContext.UsingSkill = Skill.None;
                RestoreDefaultCursor();
            }
        }
    }
    
    private void OnInventoryContainerSlotPressed(int index)
    {
        UseInventoryItem(true);
    }

    private void UseInventoryItem(bool doubleClick)
    {
        if(!CanUseInventoryItem(doubleClick))
            return;

        if (GameContext.Trading || GameContext.GamePaused)
            return;
        
        int slot = InventoryContainer.SelectedSlot;
        if (slot == Declares.InvalidSlot) 
            return;
        
        NetworkClient.Instance.SenUseItem(slot + 1);
    }

    private bool CanUseInventoryItem(bool doubleClick)
    {
        return doubleClick 
            ? GameContext.Intervals.RequestUseItemWithDoubleClick()
            : GameContext.Intervals.RequestUseItemWithU();
    }

    private void RestoreDefaultCursor()
    {
        
    }

    public void WriteToConsole(string message, FontData font)
    {
        string bbcode = FormatBBCode(message, font);

        if (!string.IsNullOrEmpty(bbcode))
        {
            ConsoleOutput.AppendText(bbcode + "\n");
        }
    }
    
    public void WriteToConsole(string message)
    {
        WriteToConsole(message, new FontData(
            Color: Colors.White,
            Bold: false,
            Italic: false));
    }

    public void OpenTrade()
    {
        TradePanelDisplay tradePanelDisplay = TradePanelDisplayScene
            .Instantiate<TradePanelDisplay>();

        tradePanelDisplay.PlayerInventory = GameContext.PlayerInventory;
        tradePanelDisplay.NpcInventory = GameContext.TradeInventory;
        AddChild(tradePanelDisplay);

        _currentPanelDisplay = tradePanelDisplay;
        GameContext.Trading = true;
    }

    public void CloseTrade()
    {
        _currentPanelDisplay?.QueueFree();
        GameContext.Trading = false;
    }
    
    public void OpenBank()
    {
        BankPanelDisplay tradePanelDisplay = BankPanelDisplayScene
            .Instantiate<BankPanelDisplay>();

        tradePanelDisplay.PlayerInventory = GameContext.PlayerInventory;
        tradePanelDisplay.BankInventory = GameContext.BankInventory;
        AddChild(tradePanelDisplay);

        _currentPanelDisplay = tradePanelDisplay;
        GameContext.Trading = true;
    }

    public void CloseBank()
    {
        _currentPanelDisplay?.QueueFree();
        GameContext.Trading = false;
    }
    
    private static string FormatBBCode(string message, FontData font)
    {
        if(string.IsNullOrEmpty(message))
            return string.Empty;
        
        string bbcode = $"[color=#{font.Color.ToHtml()}]{message}[/color]";
        
        if (font.Italic)
            bbcode = $"[i]{bbcode}[/i]";
        
        if (font.Bold)
            bbcode = $"[b]{bbcode}[/b]";
        
        return bbcode;
    }

    public void UpdateBankGold(int gold)
    {
        
    }
}