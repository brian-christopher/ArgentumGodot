namespace ArgentumOnline.Net.Commands;

public class ChangeSpellSlotCommand : ICommand
{
    public int Slot { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SpellId { get; set; }
    
    public void Unpack(Reader reader)
    {
        Slot = reader.ReadByte();
        SpellId = reader.ReadInteger();
        Name = reader.ReadString();
    }
}