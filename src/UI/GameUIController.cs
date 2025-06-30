using System;
using System.Threading.Tasks;
using ArgentumOnline.Data;
using Godot;

namespace ArgentumOnline.UI;

public partial class GameUIController : CanvasLayer
{
    [Export] public Camera2D MainCamera { get; set; }
    [Export] public RichTextLabel ConsoleOutput { get; set; }
    
    public void WriteToConsole(string message, FontData font)
    {
        string bbcode = FormatBBCode(message, font);

        if (!string.IsNullOrEmpty(bbcode))
        {
            ConsoleOutput.AppendText(bbcode + "\n");
        }
    }
    
    public void WriteToConsole(string message)
    {
        WriteToConsole(message, new FontData(
            Color: Colors.White,
            Bold: false,
            Italic: false));
    }
    
    private static string FormatBBCode(string message, FontData font)
    {
        if(string.IsNullOrEmpty(message))
            return string.Empty;
        
        string bbcode = $"[color=#{font.Color.ToHtml()}]{message}[/color]";
        
        if (font.Italic)
            bbcode = $"[i]{bbcode}[/i]";
        
        if (font.Bold)
            bbcode = $"[b]{bbcode}[/b]";
        
        return bbcode;
    }
}