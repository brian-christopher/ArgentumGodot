namespace ArgentumOnline.Data;

public sealed class PlayerStats
{
    public int MinHP { get; set; }
    public int MaxHP { get; set; }

    public bool IsAlive => MinHP > 0;
}