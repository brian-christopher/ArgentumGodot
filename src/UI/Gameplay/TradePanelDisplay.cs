using System;
using System.Runtime.CompilerServices;
using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Data;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.UI.Gameplay;

/// <summary>
/// Represents the trade panel UI in the gameplay interface.
/// It manages and displays both player and NPC inventories during a trade interaction.
/// </summary>
public partial class TradePanelDisplay : Panel
{
    #region Exported Properties
    [Export] private InventoryContainerDisplay PlayerInventoryDisplay { get; set; }
    [Export] private InventoryContainerDisplay NpcInventoryDisplay { get; set; }
    [Export] private Label ObjectInfoLabel { get; set; }
    [Export] private Label ObjectPropertiesLabel { get; set; }
    [Export] private SpinBox QuantitySpinBox { get; set; }
    #endregion
    
    public InventoryData PlayerInventory { get; set; }
    public InventoryData NpcInventory { get; set; }

    private int Quantity => (int)QuantitySpinBox.Value;

    public override void _Ready()
    {
        PlayerInventoryDisplay.SetInventory(PlayerInventory);
        PlayerInventoryDisplay.SlotPressed += i => UpdateObjectProperties(PlayerInventory[i].Item);
        
        NpcInventoryDisplay.SetInventory(NpcInventory);
        NpcInventoryDisplay.SlotPressed += i => UpdateObjectProperties(NpcInventory[i].Item);

        QuantitySpinBox.MinValue = 1;
        QuantitySpinBox.MaxValue = Declares.MaxInventoryObjs;
    }

    private void UpdateObjectProperties(ItemData item)
    {
        string text = String.Empty;
        
        if (!string.IsNullOrEmpty(item.Name))
        {
            text += item.Name;
            text += $"\n💰{item.Price}"; 
        }
        
        ObjectInfoLabel.Text = text;

        text = string.Empty;

        if (item.IsWeapon)
        {
            text += $"🗡️{item.MinHit}/{item.MaxHit}\n";
        }
        if (item.IsArmour)
        {
            text += $"🛡️️{item.MinDef}/{item.MaxDef}";
        }

        ObjectPropertiesLabel.Text = text;
    }

    private void OnBuyButtonPressed()
    {
        if (NpcInventoryDisplay.SelectedSlot != -1)
        {
            NetworkClient.Instance.SendCommerceBuy(NpcInventoryDisplay.SelectedSlot + 1, Quantity);
        }
    }

    private void OnSellButtonPressed()
    {
        if (PlayerInventoryDisplay.SelectedSlot != -1)
        {
            NetworkClient.Instance.SendCommerceSell(PlayerInventoryDisplay.SelectedSlot + 1, Quantity);
        }
    }

    private void OnCloseButtonPressed()
    {
        NetworkClient.Instance.SendCommerceEnd();
    }
}