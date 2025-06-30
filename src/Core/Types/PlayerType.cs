using System;

namespace ArgentumOnline.Core.Types;

[Flags]
public enum PlayerType
{
    None = 0,
    User = 1 << 0,
    Consejero = 1 << 1,
    SemiDios = 1 << 2,
    Dios = 1 << 3,
    Admin = 1 << 4,
    RoleMaster = 1 << 5,
    ChaosCouncil = 1 << 6,
    RoyalCouncil = 1 << 7
}

[Flags]
public enum NickColor 
{
    None = 0,
    IeCriminal = 1 << 0, 
    IeCiudadano = 1 << 1,
    IeAtacable = 1 << 2  
}