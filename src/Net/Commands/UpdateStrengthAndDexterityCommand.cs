namespace ArgentumOnline.Net.Commands;

public class UpdateStrengthAndDexterityCommand : ICommand
{ 
    public int Strength { get; set; } 
    public int Dexterity { get; set; } 
    public void Unpack(Reader reader)
    {
        Strength = reader.ReadByte();
        Dexterity = reader.ReadByte();
    }
}