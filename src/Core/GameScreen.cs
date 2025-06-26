using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.Core;

public partial class GameScreen : Node
{
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