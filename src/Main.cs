using ArgentumOnline.Core;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline;

public partial class Main : Node
{
    public override void _Ready()
    { 
        RenderingServer.SetDefaultClearColor(Colors.Black);
        GameAssets.Load();
        
        if (!DirAccess.DirExistsAbsolute("user://screenshots"))
            DirAccess.MakeDirAbsolute("user://screenshots");
    }
}