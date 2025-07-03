namespace ArgentumOnline.Net.Commands;

public class UpdateHpCommand : ICommand
{
    public int MinHp { get; set; }
    public void Unpack(Reader reader)
    {
        MinHp = reader.ReadInteger();
    }
}