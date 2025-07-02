namespace ArgentumOnline.Net.Commands;

public class BankInitCommand : ICommand
{
    public int Gold { get; set; }
    
    public void Unpack(Reader reader)
    {
        Gold = reader.ReadLong();
    }
}