using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Entities;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.Core;

public partial class GameScreen : Node
{
    [Export] public MapContainer MapContainer { get; set; }
    [Export] public Camera2D MainCamera { get; set; }

    private int _mainCharacterId = -1;
        
    private readonly CommandDispatcher _dispatcher;

    public GameScreen()
    {
        _dispatcher = new(this);
    }
    
    public override void _Ready()
    {
        NetworkClient.Instance.OnConnect += InstanceOnOnConnect;
        NetworkClient.Instance.OnDisconnect += InstanceOnOnDisconnect;
        NetworkClient.Instance.OnReceive += InstanceOnOnReceive;
        
        NetworkClient.Instance.ConnectToHost("127.0.0.1", 7666);
    }
}