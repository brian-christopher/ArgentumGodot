using ArgentumOnline.Core.Types;

namespace ArgentumOnline.Net.Commands;

public class MultiMessageCommand : ICommand
{
    public int Index { get; set; }
    
    public int Arg1 { get; set; }
    public int Arg2 { get; set; }
    public int Arg3 { get; set; }
    
    public string StringArg1 { get; set; } = string.Empty;
    
    public void Unpack(Reader reader)
    {
        Index = reader.ReadByte();
        
        switch ((Messages)Index)
        {
            case Messages.NpcHitUser:
                Arg1 = reader.ReadByte();
                Arg2 = reader.ReadInteger();
                break;
            
            case Messages.UserHitNpc:
                Arg1 = reader.ReadLong(); 
                break;
            
            case Messages.UserAttackedSwing:
                Arg1 = reader.ReadInteger(); 
                break;
            
            case Messages.UserHittedByUser:
            case Messages.UserHittedUser:
                Arg1 = reader.ReadInteger(); 
                Arg2 = reader.ReadByte(); 
                Arg3 = reader.ReadInteger(); 
                break;
            
            case Messages.WorkRequestTarget:
                Arg1 = reader.ReadInteger(); 
                break;
                
            case Messages.HaveKilledUser:
                Arg1 = reader.ReadInteger(); 
                Arg2 = reader.ReadLong(); 
                break;
            
            case Messages.UserKill:
                Arg1 = reader.ReadInteger();  
                break;
            
            case Messages.GoHome:
                Arg1 = reader.ReadByte(); 
                Arg2 = reader.ReadInteger();  
                StringArg1 = reader.ReadString();
                break;
        }
    }
}