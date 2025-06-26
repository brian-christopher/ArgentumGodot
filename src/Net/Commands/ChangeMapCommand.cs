namespace ArgentumOnline.Net.Commands;

public class ChangeMapCommand : ICommand
{
    public int Id { get; set; }
    public int Version { get; set; }
    
    public void Unpack(Reader reader)
    {
        Id = reader.ReadInteger();
        Version = reader.ReadInteger();
    }
}