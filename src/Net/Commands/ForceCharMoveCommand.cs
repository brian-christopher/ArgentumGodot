namespace ArgentumOnline.Net.Commands;

public class ForceCharMoveCommand : ICommand
{
    public int Heading { get; set; }
 
    public void Unpack(Reader reader)
    {
        Heading = reader.ReadByte();
    }
}