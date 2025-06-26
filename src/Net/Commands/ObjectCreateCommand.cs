namespace ArgentumOnline.Net.Commands;

public class ObjectCreateCommand : ICommand
{
    public int X { get; set; }
    public int Y { get; set; }
    public int GrhIndex { get; set; }
    
    public void Unpack(Reader reader)
    {
        X = reader.ReadByte();
        Y = reader.ReadByte();
        GrhIndex = reader.ReadInteger();
    }
}