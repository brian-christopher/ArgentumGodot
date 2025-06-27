using System.Linq;
using ArgentumOnline.Core;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Core.Extensions;
using Godot;

namespace ArgentumOnline.Entities.Character;

/// <summary>
/// Represents a controller responsible for managing character behavior and properties.
/// </summary>
public partial class CharacterController : Node2D
{
    private bool _isCharacterInvisible;

    #region Exported Properties
    [Export] public CharacterRenderer Renderer { get; set; }
    [Export] public CharacterEffect Effect { get; set; }
    
    [Export] private Label DialogueLabel { get; set; }
    [Export] private Timer DialogueClearTimer { get; set; }
    
    [Export] private Label NameLabel { get; set; }
    #endregion
    
    public int CharacterId { get; set; }
    public Vector2I GridPosition { get; set; }
    
    public Vector2 TargetPosition { get; set; }
    public bool IsMoving { get; private set; }
    public float Speed { get; set; } = Declares.DefaultSpeed;
    
    public int Privileges { get; set; }
    
    public Rect2 Boundaries =>
        new Rect2(
            x: GlobalPosition.X - 16,
            y: GlobalPosition.Y - 64,
            width: 32,
            height: 64);

    public bool IsDead 
        => Renderer.Head is Declares.CabezaCasper or Declares.CuerpoFragataFantasmal;

    public bool IsSailing
        => Declares.VesselIds.Contains(Renderer.Body);
    
    public bool CharacterInvisible
    {
        get => IsCharacterInvisible();
        set => SetCharacterInvisible(value);
    }

    public string CharacterName
    {
        set => NameLabel.Text = value;
        get => NameLabel.Text;
    }

    public Color CharacterNameColor
    {
        set => NameLabel.SelfModulate = value;
        get => NameLabel.SelfModulate;
    }
    
    public override void _Ready()
    {
        DialogueClearTimer.Timeout += () =>
        {
            DialogueLabel.Text = string.Empty;
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessAnimation();
        ProcessMovement((float)delta);
    }
    
    public void MoveTo(Heading heading)
    {
        if (IsMoving)
        {
            Position = TargetPosition;
            IsMoving = false;
        }

        IsMoving = true;
        
        Vector2 offset = heading.ToVector2();
        TargetPosition = Position + (offset * Declares.TileSize);
    }

    public void StopMoving()
    {
        if (IsMoving)
        {
            Position = TargetPosition;
        }
        IsMoving = false;
    }

    private void SetCharacterInvisible(bool invisible)
    {
        _isCharacterInvisible = invisible;
        
        Renderer.Visible = invisible;
        NameLabel.Visible = invisible;
    }

    private bool IsCharacterInvisible()
    {
        return _isCharacterInvisible;
    }
    
    private void ProcessMovement(float delta)
    {
        if (IsMoving)
        {
            Position = Position.MoveToward(TargetPosition, Speed * delta);
            if (Position == TargetPosition)
            {
                IsMoving = false;
            }
        }
    }

    private void ProcessAnimation()
    {
        if (IsMoving)
        {
            Renderer.Play();
        }
        else
        {
            Renderer.Stop();
        }
    }

    public void Talk(string text, Color color)
    {
        DialogueLabel.Text = text;
        DialogueLabel.Modulate = color;
        
        DialogueClearTimer.Start();
    }
}