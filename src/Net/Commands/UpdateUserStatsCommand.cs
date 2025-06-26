namespace ArgentumOnline.Net.Commands;

public class UpdateUserStatsCommand : ICommand
{
    public int MaxHp { get; set; } 
    public int MinHp { get; set; }
    
    public int MaxMana { get; set; } 
    public int MinMana { get; set; }
    
    public int MaxStamina { get; set; } 
    public int MinStamina { get; set; }

    public int Gold { get; set; }
    
    public int Level { get; set; }
    public int Experience { get; set; }
    public int ExperienceForNextLevel { get; set; }
    
    public void Unpack(Reader reader)
    {
        MaxHp = reader.ReadInteger();
        MinHp = reader.ReadInteger();
    
        MaxMana = reader.ReadInteger();
        MinMana = reader.ReadInteger();
        
        MaxStamina = reader.ReadInteger();
        MinStamina = reader.ReadInteger();

        Gold = reader.ReadLong();

        Level = reader.ReadByte();
        ExperienceForNextLevel = reader.ReadLong();
        Experience = reader.ReadLong();
    }
}