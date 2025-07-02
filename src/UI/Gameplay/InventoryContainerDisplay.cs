using System;
using System.Linq;
using ArgentumOnline.Data;
using Godot;

namespace ArgentumOnline.UI.Gameplay;

public partial class InventoryContainerDisplay : GridContainer
{
    #region Exported Properties
    [Export] private PackedScene ItemSlotDisplaysScene { get; set; }
    #endregion
    
    public InventoryData InventoryData { get; private set; }
    public int SelectedSlot { get; private set; } = -1;
    public event Action<int> SlotPressed;

    public override void _ExitTree()
    {
        if (InventoryData != null)
        {
            InventoryData.SlotChanged -= OnInventoryDataSlotChanged;
        }
    }

    public void SetInventory(InventoryData inventoryData)
    {
        InventoryData = inventoryData;
        InventoryData.SlotChanged += OnInventoryDataSlotChanged;

        for (int i = 0; i < inventoryData.Length; i++)
        {
            int index = i;
            ItemSlotDisplay itemSlotDisplay = ItemSlotDisplaysScene
                .Instantiate<ItemSlotDisplay>();
            AddChild(itemSlotDisplay);
            
            itemSlotDisplay.Index = i;
            itemSlotDisplay.Pressed += () => { OnItemSlotDisplayPressed(index); };
            OnInventoryDataSlotChanged(i, InventoryData[i]);
        }
    }

    private ItemSlotDisplay GetItemSlotDisplay(int index)
    {
        return GetChildren()
            .OfType<ItemSlotDisplay>()
            .FirstOrDefault(i => i.Index == index);
    }

    private void OnItemSlotDisplayPressed(int index)
    {
        GetItemSlotDisplay(SelectedSlot)?.SetSelected(false);
        GetItemSlotDisplay(index)?.SetSelected(true);
        
        SelectedSlot = index;
        SlotPressed?.Invoke(index);
    }

    private void OnInventoryDataSlotChanged(int index, ItemStack itemStack)
    {
        ItemSlotDisplay itemSlotDisplay = GetItemSlotDisplay(index);
        
        itemSlotDisplay?.SetEquipped(itemStack.Equipped);
        itemSlotDisplay?.SetQuantity(itemStack.Quantity);
        itemSlotDisplay?.SetIcon(itemStack.Item.Icon);
    }
}