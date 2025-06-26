using ArgentumOnline.Core;
using Godot;

namespace ArgentumOnline;

public partial class Main : Node
{
    public override void _Ready()
    { 
        RenderingServer.SetDefaultClearColor(Colors.Black);
        
        if (!DirAccess.DirExistsAbsolute("user://screenshots"))
            DirAccess.MakeDirAbsolute("user://screenshots");
    }
}