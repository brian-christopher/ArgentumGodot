using System;
using System.Threading.Tasks;
using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Extensions;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Data;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.UI;

public partial class GameUIController : CanvasLayer
{
    [Export] public Camera2D MainCamera { get; set; }
    [Export] public RichTextLabel ConsoleOutput { get; set; }
    
    public GameContext GameContext { get; set; }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton button)
        {
            HandleEventMouseButton(button);
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
}