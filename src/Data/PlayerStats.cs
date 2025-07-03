namespace ArgentumOnline.Data;

public sealed class PlayerStats
{
    public int MinHp { get; set; }
    public int MaxHp { get; set; }

    public bool IsAlive => MinHp > 0;
}