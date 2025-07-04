namespace ArgentumOnline.Data;

public sealed class PlayerStats
{
    public int MinHp { get; set; }
    public int MaxHp { get; set; }

    public int MinMp { get; set; }
    public int MaxMp { get; set; }

    public bool IsAlive => MinHp > 0;
}