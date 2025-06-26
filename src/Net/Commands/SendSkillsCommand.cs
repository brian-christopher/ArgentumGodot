namespace ArgentumOnline.Net.Commands;

public class SendSkillsCommand : ICommand
{
    private const int MaxSkills = 20;
    
    public record struct Skill(
        int Level,
        int Experiences);
    
    public Skill[] Skills { get; set; } = new Skill[MaxSkills];
    
    public void Unpack(Reader reader)
    {
        for (int i = 0; i < MaxSkills; i++)
        {
            Skills[i] = new Skill(reader.ReadByte(), reader.ReadByte());
        }
    }
}