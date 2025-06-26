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
}