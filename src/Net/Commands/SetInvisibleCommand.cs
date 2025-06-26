namespace ArgentumOnline.Net.Commands;

public class SetInvisibleCommand : ICommand
{
    public int CharIndex { get; set; }
    public bool Invisible { get; set; }
    
    public void Unpack(Reader reader)
    {        
        CharIndex = reader.ReadInteger();
        Invisible = reader.ReadBoolean();
    }
}