using System;

namespace ArgentumOnline.Core.Types;

[Flags]
public enum TileState
{
    None = 0,
    Blocked = 1 << 0, 
    Roof = 1 << 1,
    Water = 1 << 2
}