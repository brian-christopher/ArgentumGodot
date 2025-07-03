namespace ArgentumOnline.Net.Commands;

public class ShowSignalCommand : ICommand
{
    public string Message { get; set; } = string.Empty;
    public int Unknown { get; set; }
    
    public void Unpack(Reader reader)
    {
        Message = reader.ReadString();
        Unknown = reader.ReadInteger();
    }
}