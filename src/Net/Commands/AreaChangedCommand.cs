namespace ArgentumOnline.Net.Commands;

public class AreaChangedCommand : ICommand
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public void Unpack(Reader reader)
    {
        X = reader.ReadByte();
        Y = reader.ReadByte();
    }
}