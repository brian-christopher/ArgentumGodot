namespace ArgentumOnline.Net.Commands;

public class BlockPositionCommand : ICommand
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Blocked { get; set; }
    
    public void Unpack(Reader reader)
    {
        X = reader.ReadByte();
        Y = reader.ReadByte();
        Blocked = reader.ReadBoolean();
    }
}