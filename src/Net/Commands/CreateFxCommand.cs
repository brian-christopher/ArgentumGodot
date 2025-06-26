namespace ArgentumOnline.Net.Commands;

public class CreateFxCommand : ICommand
{
    public int CharIndex { get; set; }
    public int Fx { get; set; }
    public int FxLoops { get; set; }
    
    public void Unpack(Reader reader)
    {
        CharIndex = reader.ReadInteger();
        Fx = reader.ReadInteger();
        FxLoops = reader.ReadInteger();
    }
}