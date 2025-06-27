using ArgentumOnline.Core.Types;

namespace ArgentumOnline.Core.Extensions;

public static class TileStateExtensions
{
    public static TileState Block(this TileState state)
    {
        return state | TileState.Blocked;
    }

    public static TileState Unblock(this TileState state)
    {
        return state & ~TileState.Blocked;
    }

    public static bool IsBlocked(this TileState state)
    {
        return (state & TileState.Blocked) == TileState.Blocked;
    }
    
    public static bool IsWater(this TileState state)
    {
        return (state & TileState.Water) == TileState.Water;
    }
}