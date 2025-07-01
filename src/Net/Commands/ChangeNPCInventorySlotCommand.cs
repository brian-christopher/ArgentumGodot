namespace ArgentumOnline.Net.Commands;

public class ChangeNPCInventorySlotCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public int Index { get; set; }
    public int Slot { get; set; }
   
    public int Quantity { get; set; }
    public int GrhIndex { get; set; }
    public int Type { get; set; }
    public int MinHit { get; set; }
    public int MaxHit { get; set; }

    public int MinDef { get; set; }
    public int MaxDef { get; set; }
    
    public int Price { get; set; }
    
    public void Unpack(Reader reader)
    {
        Slot = reader.ReadByte();
        Name = reader.ReadString();
        Quantity = reader.ReadInteger();
        Price = (int)reader.ReadSingle();
        GrhIndex = reader.ReadInteger();
        Index = reader.ReadInteger();
        Type = reader.ReadByte();
        MaxHit = reader.ReadInteger();
        MinHit = reader.ReadInteger();
        MaxDef = reader.ReadInteger();
        MinDef = reader.ReadInteger();
    }
}