namespace ArgentumOnline.Net.Commands;

public class PlayWaveCommand : ICommand
{
    public int WaveId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public void Unpack(Reader reader)
    {
        WaveId = reader.ReadByte();
        X = reader.ReadByte();
        Y = reader.ReadByte();
    }
}