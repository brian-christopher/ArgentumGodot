using System;
using System.Collections.Generic;
using System.Linq;
using ArgentumOnline.Core;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Entities.Character;
using Godot;

namespace ArgentumOnline.Entities;

public partial class MapContainer : Node2D
{
    private const string GridPositionKey = "GridPosition";
    
    private readonly List<CharacterController> _characters = new();
    private readonly List<Node2D> _objects = new();
    
    private TileState[] _tiles = new TileState[Declares.MapSize * Declares.MapSize];

    private Node2D _currentView;
    private Node2D _viewContainer;
    
    public override void _Ready()
    {
        Array.Fill(_tiles, TileState.Blocked);
        
        _viewContainer = new();
        AddChild(_viewContainer);
    }

    public void Load(int mapId)
    {
        RemoveAllEntities();
        
        _currentView?.QueueFree();
        _currentView = ResourceLoader
            .Load<PackedScene>($"res://scenes/maps/map_{mapId}.tscn")
            .Instantiate<Node2D>();

        _tiles = _currentView.GetMeta("data")
            .AsByteArray()
            .Select(t => (TileState)t)
            .ToArray();
        
        _viewContainer.AddChild(_currentView);
    }
    
    public TileState GetTile(int x, int y)
    {
        return _tiles[x + y * Declares.MapSize];
    }

    public void SetTile(int x, int y, TileState state)
    {
        _tiles[x + y * Declares.MapSize] = state;
    }
    
    public void AddCharacter(CharacterController character)
    {
        _characters.Add(character);
        GetLayer("Layer3").AddChild(character);
    }
    
    public void RemoveCharacter(int characterId)
    {
        CharacterController character = GetCharacter(characterId);
        
        if (character != null)
        {
            _characters.Remove(character);
            character.QueueFree();
        }
    }

    public CharacterController GetCharacter(int characterId)
    {
        return _characters
            .FirstOrDefault(character => character.CharacterId == characterId);
    }

    public CharacterController GetCharacterAt(int x, int y)
    {
        return _characters
            .FirstOrDefault(character => character.GridPosition == new Vector2I(x, y));
    }

    public void AddObject(int grhId, int x, int y)
    {
        if (grhId > 0)
        {
            if (GameAssets.GrhDataList[grhId].FrameCount > 1)
            {
                grhId = GameAssets.GrhDataList[grhId].Frames[1];
            }

            GrhData grhData = GameAssets.GrhDataList[grhId];
            Sprite2D sprite = CreateSprite(grhData, x - 1, y - 1);
            
            sprite.SetMeta(GridPositionKey, new Vector2I(x, y));
            _objects.Add(sprite);

            if (sprite.RegionRect.Size == new Vector2(Declares.TileSize, Declares.TileSize))
            {
                GetLayer("Layer2").AddChild(sprite);
            }
            else
            {
                GetLayer("Layer3").AddChild(sprite);
            }
        }
    }

    public void RemoveObject(int x, int y)
    {
        Node2D node = _objects
            .FirstOrDefault(o => o.GetMeta(GridPositionKey).AsVector2I() == new Vector2I(x, y));
        
        if (node != null)
        {
            _objects.Remove(node);
            node.QueueFree();
        }
    }

    public void RemoveAllEntities()
    {
        foreach (CharacterController character in _characters)
        {
            character?.QueueFree();
        }

        foreach (Node2D @object in _objects)
        {
            @object?.QueueFree();
        }
        
        _characters.Clear();
        _objects.Clear();
    }

    private Sprite2D CreateSprite(GrhData grhData, int x, int y)
    {
        Sprite2D sprite = new();
        
        sprite.Texture = GameAssets.GetTexture(grhData.FileId);
        sprite.Position = new Vector2((x * Declares.TileSize) + 16, (y * Declares.TileSize) + Declares.TileSize);
        sprite.RegionEnabled = true;
        sprite.RegionRect = grhData.Region;
        sprite.Offset = new Vector2(0, -sprite.RegionRect.Size.Y / 2);
        
        return sprite;
    }

    private Node2D GetLayer(string layerName)
    {
        Node2D node = _currentView
            .GetChildren()
            .OfType<Node2D>()
            .FirstOrDefault(node => node.Name == layerName);
        
        return node ?? throw new Exception($"Layer {layerName} not found.");   
    }
}