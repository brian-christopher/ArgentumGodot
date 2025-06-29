using Godot;

namespace ArgentumOnline.Net.Commands;

public class ChatOverHeadCommand : ICommand
{
    public string Message { get; set; } = string.Empty;
    public int CharIndex { get; set; }
    public Color Color { get; set; }
    
    public void Unpack(Reader reader)
    {
        Message = reader.ReadString();
        CharIndex = reader.ReadInteger();
        Color = Color.Color8(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
    }
}