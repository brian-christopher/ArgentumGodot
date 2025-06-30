namespace ArgentumOnline.Net.Commands;

public class ShowMessageBoxCommand : ICommand
{
    public string Message { get; set; } = string.Empty;
    
    public void Unpack(Reader reader)
    {
        Message = reader.ReadString();
    }
}