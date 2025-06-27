using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Entities;
using ArgentumOnline.Entities.Character;
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

    public override void _Process(double delta)
    {
        UpdateCameraPosition((float)delta);
    }

    private void UpdateCameraPosition(float delta)
    {
        CharacterController mainCharacter = MapContainer.GetCharacter(_mainCharacterId);
        
        if (mainCharacter != null && MainCamera != null)
        {
            MainCamera.Position = mainCharacter.Position;
        }
    }
}