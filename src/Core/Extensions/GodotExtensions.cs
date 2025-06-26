using ArgentumOnline.Core.Types;
using Godot;

namespace ArgentumOnline.Core.Extensions;

public static class GodotExtensions
{
    public static Vector2 ToVector2(this Heading heading)
    {
        return heading switch
        {
            Heading.South => Vector2.Up,
            Heading.North => Vector2.Down,
            Heading.West => Vector2.Left,
            Heading.East => Vector2.Right,
            _ => Vector2.Zero,
        };
    }
}