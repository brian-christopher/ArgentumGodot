namespace ArgentumOnline.Net.Commands;

public class PlayMidiCommand : ICommand
{
    public int MidiId { get; set; }
    public int Loops { get; set; }
    
    public void Unpack(Reader reader)
    {
        MidiId = reader.ReadInteger();
        Loops = reader.ReadInteger();
    }
}