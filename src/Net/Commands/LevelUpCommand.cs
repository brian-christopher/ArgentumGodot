namespace ArgentumOnline.Net.Commands;

public class LevelUpCommand : ICommand
{
    public int SkillPoints { get; set; }
    
    public void Unpack(Reader reader)
    {
        SkillPoints = reader.ReadInteger();
    }
}