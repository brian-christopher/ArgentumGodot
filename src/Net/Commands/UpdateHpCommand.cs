namespace ArgentumOnline.Net.Commands;

public class UpdateHpCommand : ICommand
{
    public int Health { get; set; }
    public void Unpack(Reader reader)
    {
        Health = reader.ReadInteger();
    }
}