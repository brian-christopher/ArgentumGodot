using System.Collections.Generic;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Extensions;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Entities;
using ArgentumOnline.Entities.Character;
using ArgentumOnline.Net;
using ArgentumOnline.UI;
using Godot;

namespace ArgentumOnline.Core;

public partial class GameScreen : Node
{
    [Export] public MapContainer MapContainer { get; set; }
    [Export] public Camera2D MainCamera { get; set; }
    [Export] public GameUIController UIController { get; set; }

    private readonly GameContext _gameContext = new();
    private int _mainCharacterId = -1;
    
    private readonly CommandDispatcher _dispatcher;

    private readonly Dictionary<string, Heading> _inputMap = new()
    {
        ["ui_left"] = Heading.West,
        ["ui_right"] = Heading.East,
        ["ui_up"] = Heading.North,
        ["ui_down"] = Heading.South,
    };

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
        
        UIController.GameContext = _gameContext;
        UIController.Initialize();
    }

    public override void _Process(double delta)
    {
        ProcessMovementInput();
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

    private void ProcessMovementInput()
    {
        if (_gameContext.Traveling || 
            _gameContext.ViewingForum || 
            _gameContext.Trading ||
            _gameContext.GamePaused)
        {
            return;
        }

        foreach (var pair in _inputMap)
        {
            if (Input.IsActionPressed(pair.Key))
            {
                MovePlayer(pair.Value);
                return;
            }
        }
    }

    private void MovePlayer(Heading heading)
    {
        if (heading == Heading.None)
            return;
        
        CharacterController character = MapContainer.GetCharacter(_mainCharacterId);
        
        if (character == null || character.IsMoving)
            return;

        Vector2I gridPosition = character.GridPosition + heading.ToVector2I();
        
        if (CanMoveTo(gridPosition.X, gridPosition.Y))
        {
            NetworkClient.Instance.SendWalk(heading);
            
            //No se porque esto esta asi. Lo unico que logra es que el personaje pegue un salto cuando camina
            //Obligando al usuario a presionar la tecla L(PosUpdate)
            if (!_gameContext.UserResting && !_gameContext.UserMeditating)
            {
                MoveCharacter(_mainCharacterId, heading);
            }
        }
        else
        {
            if (character.Renderer.Heading != heading)
            {
                NetworkClient.Instance.SendChangeHeading(heading);
            }
        }
        
        //	_gameInput.minimap.update_player_position(character.gridPosition.x, character.gridPosition.y)
    }
    
    private bool CanMoveTo(int x, int y)
    {
        TileState tile = MapContainer.GetTile(x - 1 , y - 1);

        if (tile.IsBlocked())
        {
            return false; 
        }
        
        CharacterController character = MapContainer.GetCharacterAt(x, y);
        CharacterController mainCharacter = MapContainer.GetCharacter(_mainCharacterId);
        Vector2I playerPosition = mainCharacter.GridPosition;

        if (character != null)
        {
            if (MapContainer.GetTile(playerPosition.X - 1, playerPosition.Y - 1).IsBlocked())
            {
                return false;
            }

            if (character.Renderer.Head != Declares.CabezaCasper &&
                character.Renderer.Body != Declares.CuerpoFragataFantasmal)
            {
                return false;
            }
            else
            {
                if (MapContainer.GetTile(playerPosition.X - 1, playerPosition.Y - 1).IsWater())
                {
                    if (MapContainer.GetTile(x - 1, y - 1).IsWater())
                    {
                        return false;
                    }
                }
                else
                {
                    if (MapContainer.GetTile(x - 1, y - 1).IsWater())
                    {
                        return false;
                    }
                }

                if (mainCharacter.Privileges is > 0 and < 6) 
                {
                    if (mainCharacter.CharacterInvisible)
                    {
                        return false;
                    }
                }
            }
        }

        if (_gameContext.UserSailing != MapContainer.GetTile(x - 1, y - 1).IsWater())
        {
            return false;
        }
        return true;
    }

    private void MoveCharacter(int characterId, Heading heading)
    {
        CharacterController character = MapContainer.GetCharacter(characterId);
        
        if (character == null)
        {
            return;
        }
        
        Vector2I offset = heading.ToVector2I();
        
        character.Renderer.Heading = heading;
        character.GridPosition += offset;
        character.MoveTo(heading);

        if (character.Effect.EffectId is >= 40 and <= 49)
        {
            character.Effect.StopEffect();
        }

        if (!character.IsDead)
        {
            Rect2 bound = MainCamera
                .GetCanvasTransform()
                .AffineInverse() * MainCamera.GetViewportRect();

            if (bound.Intersects(character.Boundaries))
            {
                if(character.IsSailing)
                    character.PlaySailingSound();
                else
                    character.PlayFootstepSound();
            }
        }
    }
}