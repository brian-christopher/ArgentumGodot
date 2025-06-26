using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Net;
using ArgentumOnline.Net.Commands;
using Godot;

namespace ArgentumOnline.Core;

public partial class GameScreen : Node
{
    private void InstanceOnOnReceive(byte[] data)
    {
        Reader reader = new(data);
        
        while (reader.Position < reader.Length)
        {
            _dispatcher.Dispatch(reader);    
        }
    }

    private void InstanceOnOnDisconnect()
    {
    }

    private void InstanceOnOnConnect()
    {
        NetworkClient
            .Instance
            .SendLoginExistingCharacter("shak", "123");
    }
    
    [Handler(ServerPacketId.Logged)]
    private void HandleLogged(LoggedCommand command)
    {
        
    }

    [Handler(ServerPacketId.MultiMessage)]
    private void HandleMultiMessage(MultiMessageCommand command)
    {
        
    }

    [Handler(ServerPacketId.ChangeInventorySlot)]
    private void HandleChangeInventorySlotCommand(ChangeInventorySlotCommand command)
    {
        
    }

    [Handler(ServerPacketId.ChangeSpellSlot)]
    private void HandleChangeSpellSlot(ChangeSpellSlotCommand command)
    {
        
    }

    [Handler(ServerPacketId.UserIndexInServer)]
    private void HandleUserIndexInServer(UserIndexInServerCommand command)
    {
        
    }

    [Handler(ServerPacketId.ChangeMap)]
    private void HandleChangeMap(ChangeMapCommand command)
    {
        
    }

    [Handler(ServerPacketId.PlayMIDI)]
    private void HandlePlayMidi(PlayMidiCommand command)
    {
        
    }

    [Handler(ServerPacketId.AreaChanged)]
    private void HandleAreaChanged(AreaChangedCommand command)
    {
        
    }

    [Handler(ServerPacketId.ObjectCreate)]
    private void HandleObjectCreate(ObjectCreateCommand command)
    {
        
    }

    [Handler(ServerPacketId.BlockPosition)]
    private void HandleBlockPosition(BlockPositionCommand command)
    {
        
    }

    [Handler(ServerPacketId.CharacterCreate)]
    private void HandleCharacterCreate(CharacterCreateCommand command)
    {
        
    }

    [Handler(ServerPacketId.SetInvisible)]
    private void HandleSetInvisible(SetInvisibleCommand command)
    {
        
    }

    [Handler(ServerPacketId.CreateFX)]
    private void HandleCreateFx(CreateFxCommand command)
    {
        
    }
    
    [Handler(ServerPacketId.UpdateStrenghtAndDexterity)]
    private void HandleUpdateStrengthAndDexterityCommand(UpdateStrengthAndDexterityCommand command)
    {}

    [Handler(ServerPacketId.UserCharIndexInServer)]
    private void HandleUserCharIndexInServer(UserCharIndexInServerCommand command)
    {
        
    }

    [Handler(ServerPacketId.GuildChat)]
    private void HandleGuildChat(GuildChatCommand command)
    {
        
    }
    
    [Handler(ServerPacketId.LevelUp)]   
    private void HandleLevelUp(LevelUpCommand command)
    {}
    
    [Handler(ServerPacketId.SendSkills)]
    private void HandleSendSkills(SendSkillsCommand command)
    {}

    [Handler(ServerPacketId.UpdateUserStats)]
    private void HandleUpdateUserStats(UpdateUserStatsCommand command)
    {
        
    }

    [Handler(ServerPacketId.UpdateHungerAndThirst)]
    private void HandleUpdateHungerAndThirstCommand(UpdateHungerAndThirstCommand command)
    {
        
    }
}