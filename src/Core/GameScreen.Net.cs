using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Extensions;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Entities.Character;
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
        MapContainer.Load(command.Id);
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
        MapContainer.RemoveOjbect(command.X, command.Y);
        MapContainer.AddObject(command.GrhIndex, command.X, command.Y);
    }

    [Handler(ServerPacketId.ErrorMsg)]
    private void HandleErrorMessage(ErrorMessageCommand command)
    {
        
    }

    [Handler(ServerPacketId.ConsoleMsg)]
    private void HandleConsoleMessage(ConsoleMessageCommand command)
    {
        
    }

    [Handler(ServerPacketId.BlockPosition)]
    private void HandleBlockPosition(BlockPositionCommand command)
    {
        TileState tile = MapContainer.GetTile(command.X - 1, command.Y - 1);
        
        tile = command.Blocked 
            ? tile.Block() 
            : tile.Unblock();

        MapContainer.SetTile(command.X - 1, command.Y - 1, tile);
    }

    [Handler(ServerPacketId.CharacterCreate)]
    private void HandleCharacterCreate(CharacterCreateCommand command)
    {
        CharacterController character = ResourceLoader
            .Load<PackedScene>("res://scenes/entities/character/character.tscn")
            .Instantiate<CharacterController>();
        
        character.CharacterName = command.Name;
        character.CharacterNameColor = Colors.White;
        character.CharacterId = command.CharIndex;
        character.Position = new Vector2((command.X - 1) * 32, (command.Y - 1) * 32) + new Vector2(16, 32);
        character.GridPosition = new Vector2I(command.X, command.Y);
        
        character.Renderer.Body = command.Body;
        character.Renderer.Head = command.Head;
        character.Renderer.Helmet = command.Helmet;
        character.Renderer.Weapon = command.Weapon;
        character.Renderer.Shield = command.Shield;
        character.Renderer.Heading = (Heading)command.Heading;
        
        MapContainer.AddCharacter(character);
    }

    [Handler(ServerPacketId.CharacterRemove)]
    private void HandleCharacterRemove(CharacterRemoveCommand command)
    {
        MapContainer.RemoveCharacter(command.CharIndex);
    }

    [Handler(ServerPacketId.SetInvisible)]
    private void HandleSetInvisible(SetInvisibleCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(command.CharIndex);
        
        if (character != null)
        {
            character.CharacterInvisible = command.Invisible;
        }
    }

    [Handler(ServerPacketId.RainToggle)]
    private void HandleRainToggle()
    {
        
    }

    [Handler(ServerPacketId.UpdateSta)]
    private void HandleUpdateSta(UpdateStaCommand command)
    {
        
    }

    [Handler(ServerPacketId.CharacterMove)]
    private void CharacterMove(CharacterMoveCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(command.CharIndex);
        
        if (character == null)
        {
            return;
        }

        static int Sgn(int x)
        {
            if (x > 0)
                return 1;
            if (x < 0)
                return -1;
            return 0;
        }
        
        int addX = command.X - character.GridPosition.X;
        int addY = command.Y - character.GridPosition.Y;

        Heading heading = Heading.South;
        if (Sgn(addX) == 1)
            heading = Heading.East;
        else if (Sgn(addX) == -1)
            heading = Heading.West;
        else if (Sgn(addY) == 1)
            heading = Heading.South;
        else if (Sgn(addY) == -1)
            heading = Heading.North;
        
        MoveCharacter(command.CharIndex, heading);
    }

    [Handler(ServerPacketId.CharacterChange)]
    private void HandleCharacterChange(CharacterChangeCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(command.CharIndex);
        
        if (character != null)
        {
            character.Renderer.Body = command.Body;
            character.Renderer.Head = command.Head;
            character.Renderer.Helmet = command.Helmet;
            character.Renderer.Weapon = command.Weapon;
            character.Renderer.Shield = command.Shield;
            character.Renderer.Heading = (Heading)command.Heading;
            
            character.Effect.PlayEffect(command.FxId, command.FxLoops);
        }
    }

    [Handler(ServerPacketId.CreateFX)]
    private void HandleCreateFx(CreateFxCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(command.CharIndex);
        
        if (character != null)
        {
            character.Effect.PlayEffect(command.Fx, command.FxLoops);
        }
    }
    
    [Handler(ServerPacketId.UpdateStrenghtAndDexterity)]
    private void HandleUpdateStrengthAndDexterityCommand(UpdateStrengthAndDexterityCommand command)
    {}

    [Handler(ServerPacketId.UserCharIndexInServer)]
    private void HandleUserCharIndexInServer(UserCharIndexInServerCommand command)
    {
        _mainCharacterId = command.CharIndex;
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