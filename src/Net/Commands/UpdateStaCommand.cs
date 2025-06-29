namespace ArgentumOnline.Net.Commands;

public class UpdateStaCommand : ICommand
{
    public int Stamina { get; set; }
    
    public void Unpack(Reader reader)
    {
        Stamina = reader.ReadInteger();
    }
}