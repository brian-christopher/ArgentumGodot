using System;
using System.Collections;
using System.Collections.Generic;

namespace ArgentumOnline.Data;
public record ItemStack(
    int Quantity,
    bool Equipped,
    ItemData Item);

public sealed class InventoryData : IEnumerable<ItemStack>
{
    private readonly ItemStack[] _slots;
    public event Action<ItemStack> OnChange;

    public InventoryData(int length)
    {
        _slots = new ItemStack[length];
    }
    
    public int Length => _slots.Length;
    
    public ItemStack this[int index]
    {
        get => _slots[index];
        
        set
        {
            _slots[index] = value;
            OnChange?.Invoke(value);
        }
    }

    public IEnumerator<ItemStack> GetEnumerator()
    {
        return ((IEnumerable<ItemStack>)_slots).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}