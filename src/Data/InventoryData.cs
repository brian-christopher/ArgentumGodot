using System;
using System.Collections;
using System.Collections.Generic;

namespace ArgentumOnline.Data;
public record ItemStack(
    int Quantity,
    bool Equipped,
    ItemData Item);

public sealed class InventoryData
{
    private readonly ItemStack[] _slots;
    public event Action<int, ItemStack> SlotChanged;

    public InventoryData(int length)
    {
        _slots = new ItemStack[length];
        
        for (int i = 0; i < length; i++)
            _slots[i] = new ItemStack(0, false, new ItemData());       
    }
    
    public int Length => _slots.Length;

    public ItemStack this[int index]
    {
        get => _slots[index];

        set
        {
            _slots[index] = value;
            SlotChanged?.Invoke(index, value);
        }
    }
}