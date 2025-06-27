using System;
using System.Runtime.InteropServices.ComTypes;
using ArgentumOnline.Core.Types;
using Godot;

namespace ArgentumOnline.Entities.Character;

/// <summary>
/// The CharacterRenderer class is responsible for rendering character elements such as helmet, head, body, weapon, and shield
/// as well as managing animations for different states and directions.
/// </summary>
public partial class CharacterRenderer : Node2D
{
    private const string DefaultPath = "res://resources/character/default.tres";
    
    #region Exported Properties

    [Export] private AnimatedSprite2D _helmetAnimator;
    [Export] private AnimatedSprite2D _headAnimator;
    [Export] private AnimatedSprite2D _bodyAnimator;
    [Export] private AnimatedSprite2D _weaponAnimator;
    [Export] private AnimatedSprite2D _shieldAnimator;

    #endregion

    private int _helmet;
    private int _head;
    private int _body;
    private int _weapon;
    private int _shield;

    public int Helmet
    {
        get => _helmet;
        set => SetHelmet(value);
    }
    
    public int Head
    {
        get => _head;
        set => SetHead(value);
    }

    public int Body
    {
        get => _body;
        set => SetBody(value);
    }

    public int Weapon
    {
        get => _weapon;
        set => SetWeapon(value);
    }
    
    public int Shield
    {
        get => _shield;
        set => SetShield(value);
    }
    
    public Heading Heading { get; set; } = Heading.South;
    
    private void SetHelmet(int value)
    {
        _helmet = value;
        _helmetAnimator.SpriteFrames = LoadSpriteFrames($"res://resources/character/helmets/helmet_{value}.tres");
    }
    private void SetHead(int value)
    {
        _head = value;
        _headAnimator.SpriteFrames = LoadSpriteFrames($"res://resources/character/heads/head_{value}.tres");
    }
    private void SetBody(int value)
    {
        _body = value;
        _bodyAnimator.SpriteFrames = LoadSpriteFrames($"res://resources/character/bodies/body_{value}.tres");

        if (_bodyAnimator
                .SpriteFrames
                .GetFrameCount("idle_south") != 0)
        {
            float offset = _bodyAnimator.SpriteFrames
                .GetFrameTexture("idle_south", 0)
                .GetHeight() / 2.0f;

            Position = Position with
            {
                Y = -offset
            };
        }
    }
    private void SetWeapon(int value)
    {
        _weapon = value;
        _weaponAnimator.SpriteFrames = LoadSpriteFrames($"res://resources/character/weapons/weapon_{value}.tres");
    }

    private void SetShield(int value)
    {
        _shield = value;
        _shieldAnimator.SpriteFrames = LoadSpriteFrames($"res://resources/character/shields/shield_{value}.tres");
    }

    public void Play()
    {
        string key = Enum.GetName(Heading)!.ToLower();
        
        _bodyAnimator.Play($"walk_{key}");
        _weaponAnimator.Play($"walk_{key}");
        _shieldAnimator.Play($"walk_{key}");
        _headAnimator.Play($"idle_{key}");
        _helmetAnimator.Play($"idle_{key}");
    }

    public void Stop()
    {
        string key = Enum.GetName(Heading)!.ToLower();
        
        _bodyAnimator.Play($"idle_{key}");
        _weaponAnimator.Play($"idle_{key}");
        _shieldAnimator.Play($"idle_{key}");
        _headAnimator.Play($"idle_{key}");
        _helmetAnimator.Play($"idle_{key}");
    }
    
    private SpriteFrames LoadSpriteFrames(string path)
    {
        if (ResourceLoader.Exists(path))
            return ResourceLoader.Load<SpriteFrames>(path);
        else
            return ResourceLoader.Load<SpriteFrames>(DefaultPath);
    }
}