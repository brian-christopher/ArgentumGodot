namespace ArgentumOnline.Net.Commands;

public class ConsoleMessageCommand : ICommand
{
    public string Message { get; set; } = string.Empty;
    public int FontIndex { get; set; }
    
    public void Unpack(Reader reader)
    {
        Message = reader.ReadString();
        FontIndex = reader.ReadByte();       
    }
}