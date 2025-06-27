using ArgentumOnline.Core.Types;
using Godot;

namespace ArgentumOnline.Core.Extensions;

public static class GodotExtensions
{
    public static Vector2 ToVector2(this Heading heading)
    {
        return ToVector2I(heading);
    }
    
    public static Vector2I ToVector2I(this Heading heading)
    {
        return heading switch
        {
            Heading.South => Vector2I.Up,
            Heading.North => Vector2I.Down,
            Heading.West => Vector2I.Left,
            Heading.East => Vector2I.Right,
            _ => Vector2I.Zero,
        };
    }
}