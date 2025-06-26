namespace ArgentumOnline.Net.Commands;

public class LoggedCommand : ICommand
{
    public int UserClass { get; set; }
    
    public void Unpack(Reader reader)
    {
        UserClass = reader.ReadByte();
    }
}