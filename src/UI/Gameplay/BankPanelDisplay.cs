using System;
using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Data;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.UI.Gameplay;


public partial class BankPanelDisplay : Panel
{
    #region Exported Properties
    [Export] private InventoryContainerDisplay PlayerInventoryContainer { get; set; }
    [Export] private InventoryContainerDisplay BankInventoryContainer { get; set; }
    [Export] private Label ObjectInfoLabel { get; set; }
    [Export] private SpinBox QuantitySpinBox { get; set; }
    #endregion
    
    public InventoryData PlayerInventory { get; set; }
    public InventoryData BankInventory { get; set; }

    private int Quantity => (int)QuantitySpinBox.Value;

    public override void _Ready()
    {
        PlayerInventoryContainer.SetInventory(PlayerInventory);
        PlayerInventoryContainer.SlotPressed += i => UpdateObjectProperties(PlayerInventory[i].Item);
        
        BankInventoryContainer.SetInventory(BankInventory);
        BankInventoryContainer.SlotPressed += i => UpdateObjectProperties(BankInventory[i].Item);

        QuantitySpinBox.MinValue = 1;
        QuantitySpinBox.MaxValue = Declares.MaxInventoryObjs;
    }

    private void UpdateObjectProperties(ItemData item)
    {
        string text = String.Empty;
        
        if (!string.IsNullOrEmpty(item.Name))
        {
            text += item.Name;
        } 
        
        if (item.IsWeapon)
        {
            text += $"\n🗡️{item.MinHit}/{item.MaxHit}";
        }
        if (item.IsArmour)
        {
            text += $"\n🛡️️{item.MinDef}/{item.MaxDef}";
        }
        
        ObjectInfoLabel.Text = text;
    }

    private void OnBuyButtonPressed()
    {
        if (BankInventoryContainer.SelectedSlot != -1)
        {
            NetworkClient.Instance.SendBankExtractItem(BankInventoryContainer.SelectedSlot + 1, Quantity);
        }
    }

    private void OnSellButtonPressed()
    {
        if (PlayerInventoryContainer.SelectedSlot != -1)
        {
            NetworkClient.Instance.SendBankDepositItem(PlayerInventoryContainer.SelectedSlot + 1, Quantity);
        }
    }

    private void OnCloseButtonPressed()
    {
        NetworkClient.Instance.SendBankEnd();
    }
}