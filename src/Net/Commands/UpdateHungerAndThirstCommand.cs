namespace ArgentumOnline.Net.Commands;

public class UpdateHungerAndThirstCommand : ICommand
{
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    
    public void Unpack(Reader reader)
    {
        reader.ReadByte();
        Thirst = reader.ReadByte();

        reader.ReadByte();
        Hunger = reader.ReadByte();
    }
}