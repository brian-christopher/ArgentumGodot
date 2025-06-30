using ArgentumOnline.Core.Types;
using ArgentumOnline.Data;

namespace ArgentumOnline.Core;

/// <summary>
/// Represents the context of the active game state, including inventories, game states,
/// and various user states. This class is designed to encapsulate the current state
/// of the game and its player, and is used to manage transitions, interactions, and gameplay flow.
/// </summary>
public sealed class GameContext
{
    //Inventories
    public InventoryData PlayerInventory { get; init; } = new(Declares.MaxInventorySlots);
    public InventoryData TradeInventory { get; init; } = new(Declares.MaxNpcInventorySlots);
    public InventoryData BankInventory { get; init; } = new(Declares.MaxBankInventorySlots);
    
    //Game states
    public bool Traveling { get; set; }
    public bool Trading { get; set; }
    public bool GamePaused { get; set; }
    public bool ViewingForum { get; set; }
    public int CurrentMap { get; set; }
    
    //User states
    public Skill UsingSkill { get; set; }
    public bool UserParalyzed { get; set; }
    public bool UserBlind { get; set; }
    public bool UserStupid { get; set; }
    public bool UserResting { get; set; }
    public bool UserMeditating { get; set; }
    public bool UserSailing { get; set; }
    public TickIntervals Intervals { get; init; } = new();
}