using System.Linq;
using ArgentumOnline.Core.Types;
using Godot;

namespace ArgentumOnline.Core.Data;

public sealed class ItemData
{
    public static readonly ItemClass[] EquipableTypes =
    [
        ItemClass.eOBJType_otWeapon,
        ItemClass.eOBJType_otESCUDO,
        ItemClass.eOBJType_otArmadura,
        ItemClass.eOBJType_otCASCO,
        ItemClass.eOBJType_otFlechas,
        ItemClass.eOBJType_otAnillo,
    ];
    
    public static readonly ItemClass[] UsableTypes =
    [
        ItemClass.eOBJType_otPociones,
        ItemClass.eOBJType_otBebidas,
        ItemClass.eOBJType_otPergaminos,
        ItemClass.eOBJType_otGuita,
        ItemClass.eOBJType_otUseOnce
    ];
    
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public Texture2D Icon { get; set; }
    public ItemClass Type { get; set; }
    
    public int MaxHit { get; set; }
    public int MinHit { get; set; }

    public int MinDef { get; set; }
    public int MaxDef { get; set; }
    
    public int Price { get; set; }

    public bool IsEquipable => EquipableTypes.Contains(Type);
    public bool IsUsable => UsableTypes.Contains(Type);
    
    public bool IsWeapon => Type == ItemClass.eOBJType_otWeapon;
    public bool IsArmour => Type == ItemClass.eOBJType_otArmadura;
}