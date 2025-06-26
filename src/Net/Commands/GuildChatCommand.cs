namespace ArgentumOnline.Net.Commands;

public class GuildChatCommand : ICommand
{
    public string Message { get; set; } = string.Empty;
    
    public void Unpack(Reader reader)
    {
        Message = reader.ReadString();
    }
}