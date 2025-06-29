namespace ArgentumOnline.Net.Commands;

public class UpdateExpCommand : ICommand
{
    public int Experience { get; set; }
    public void Unpack(Reader reader)
    {
        Experience = reader.ReadLong();
    }
}