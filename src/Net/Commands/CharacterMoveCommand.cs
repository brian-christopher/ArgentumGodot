namespace ArgentumOnline.Net.Commands;

public class CharacterMoveCommand : ICommand
{
    public int CharIndex { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public void Unpack(Reader reader)
    {
        CharIndex = reader.ReadInteger();
        X = reader.ReadByte();
        Y = reader.ReadByte();
    }
}