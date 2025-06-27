namespace ArgentumOnline.Net.Commands;

public class CharacterRemoveCommand : ICommand
{
    public int CharIndex { get; set; }
    
    public void Unpack(Reader reader)
    {
        CharIndex = reader.ReadInteger();
    }
}