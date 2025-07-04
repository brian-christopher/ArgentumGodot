using System.Linq;
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
            Heading.South => Vector2I.Down,
            Heading.North => Vector2I.Up,
            Heading.West => Vector2I.Left,
            Heading.East => Vector2I.Right,
            _ => Vector2I.Zero,
        };
    }

    public static Vector2I ToVector2I(this Vector2 vector)
    {
        return new Vector2I((int)vector.X, (int)vector.Y);
    }
    
    public static bool IsNodeInstanced<T>(this Node node) where T : Node
    {
        return node.GetChildren()
            .OfType<T>()
            .Any();
    }
}