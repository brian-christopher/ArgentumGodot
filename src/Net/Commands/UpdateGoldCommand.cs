namespace ArgentumOnline.Net.Commands;

public class UpdateGoldCommand : ICommand
{
    public int Gold { get; set; }
    public void Unpack(Reader reader)
    {
        Gold = reader.ReadLong();
    }
}