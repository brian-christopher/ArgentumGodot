namespace ArgentumOnline.Net.Commands;

public class CharacterChangeCommand : ICommand
{
    public int CharIndex { get; set; }
    public int Heading { get; set; }
    
    public int Body { get; set; }
    public int Head { get; set; }
    public int Helmet { get; set; }
    public int Shield { get; set; }
    public int Weapon { get; set; }
    
    public int FxId { get; set; }
    public int FxLoops { get; set; }
    
    public void Unpack(Reader reader)
    {
        CharIndex = reader.ReadInteger();
        Body = reader.ReadInteger();
        Head = reader.ReadInteger();
        Heading = reader.ReadByte();
        Weapon = reader.ReadInteger();
        Shield = reader.ReadInteger();
        Helmet = reader.ReadInteger();
        FxId = reader.ReadInteger();
        FxLoops = reader.ReadInteger();
    }
}