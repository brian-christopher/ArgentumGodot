using System;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Extensions;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Data;
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
        Messages message = (Messages)command.Index;
        switch (message)
        {
            case Messages.SafeModeOn:
                UIController.WriteToConsole(">>SEGURO ACTIVADO<<",
                    new FontData(Colors.Green, true, false));
                break;

            case Messages.SafeModeOff:
                UIController.WriteToConsole(">>SEGURO DESACTIVADO<<",
                    new FontData(Colors.Green, true, false));
                break;

            case Messages.ResuscitationSafeOn:
                UIController.WriteToConsole("SEGURO DE RESURRECCION ACTIVADO",
                    new FontData(Colors.Green, true, false));
                break;

            case Messages.ResuscitationSafeOff:
                UIController.WriteToConsole("SEGURO DE RESURRECCION DESACTIVADO",
                    new FontData(Colors.Green, true, false));
                break;

            case Messages.NpcSwing:
                UIController.WriteToConsole("¡¡¡La criatura falló el golpe!!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.NpcKillUser:
                UIController.WriteToConsole("¡¡La criatura te ha matado!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.BlockedWithShieldUser:
                UIController.WriteToConsole("¡¡Has rechazado el ataque con el escudo!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.BlockedWithShieldOther:
                UIController.WriteToConsole("¡¡El usuario rechazó el ataque con su escudo!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.UserSwing:
                UIController.WriteToConsole("¡¡Has fallado el golpe!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.NobilityLost:
                UIController.WriteToConsole("¡¡Has perdido tu nobleza!!",
                    new FontData(Colors.Red, false, false));
                break;

            case Messages.NpcHitUser:
                switch (command.Arg1)
                {
                    case Declares.bCabeza:
                        UIController.WriteToConsole($"¡¡La criatura te ha pegado en la cabeza por {command.Arg2} !!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bBrazoIzquierdo:
                    case Declares.bBrazoDerecho:
                        UIController.WriteToConsole($"¡¡La criatura te ha pegado en el brazo por {command.Arg2} !!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bPiernaIzquierda:
                    case Declares.bPiernaDerecha:
                        UIController.WriteToConsole($"¡¡La criatura te ha pegado en la pierna por {command.Arg2} !!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bTorso:
                        UIController.WriteToConsole($"¡¡La criatura te ha pegado en torso por {command.Arg2} !!",
                            new FontData(Colors.Red, true, false));
                        break;
                }

                break;

            case Messages.UserHitNpc:
                UIController.WriteToConsole($"¡¡Le has pegado a la criatura por {command.Arg1} !!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.UserAttackedSwing:
                UIController.WriteToConsole(
                    $"¡¡{MapContainer.GetCharacter(command.Arg1)?.CharacterName} te atacó y falló!!",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.UserHittedByUser:
                string characterName = MapContainer.GetCharacter(command.Arg1)?.CharacterName ?? string.Empty;
                int damage = command.Arg3;

                switch (command.Arg2)
                {
                    case Declares.bCabeza:
                        UIController.WriteToConsole($"¡¡{characterName} te ha pegado en la cabeza por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bBrazoIzquierdo:
                    case Declares.bBrazoDerecho:
                        UIController.WriteToConsole($"¡¡{characterName} te ha pegado en el brazo por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bPiernaIzquierda:
                    case Declares.bPiernaDerecha:
                        UIController.WriteToConsole($"¡¡{characterName} te ha pegado en la pierna por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bTorso:
                        UIController.WriteToConsole($"¡¡{characterName} te ha pegado en el torso por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;
                }

                break;

            case Messages.UserHittedUser:
            {
                characterName = MapContainer.GetCharacter(command.Arg1)?.CharacterName ?? string.Empty;
                damage = command.Arg3;

                switch (command.Arg2)
                {
                    case Declares.bCabeza:
                        UIController.WriteToConsole($"¡¡Le has pegado a {characterName} en la cabeza por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bBrazoIzquierdo:
                    case Declares.bBrazoDerecho:
                        UIController.WriteToConsole($"¡¡Le has pegado a {characterName} en el brazo por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bPiernaIzquierda:
                    case Declares.bPiernaDerecha:
                        UIController.WriteToConsole($"¡¡Le has pegado a {characterName} en la pierna por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;

                    case Declares.bTorso:
                        UIController.WriteToConsole($"¡¡Le has pegado a {characterName} en la cabeza por {damage}!!",
                            new FontData(Colors.Red, true, false));
                        break;
                }

                break;
            }

            case Messages.WorkRequestTarget:
                _gameContext.UsingSkill = (Skill)command.Arg1;

                command.StringArg1 = _gameContext.UsingSkill switch
                {
                    Skill.Magia => "Haz click sobre el objetivo...",
                    Skill.Pesca => "Haz click sobre el sitio donde quieres pescar...",
                    Skill.Robar => "Haz click sobre la víctima...",
                    Skill.Talar => "Haz click sobre el árbol...",
                    Skill.Mineria => "Haz click sobre el yacimiento...",
                    Skill.Domar => "Haz click sobre la criatura...",
                    Skill.FundirMetal => "Haz click sobre la fragua...",
                    Skill.Proyectiles => "Haz click sobre la victima...",
                };
                UIController.WriteToConsole(command.StringArg1,
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.HaveKilledUser:
                characterName = MapContainer.GetCharacter(command.Arg1).CharacterName ?? string.Empty;

                UIController.WriteToConsole($"Has matado a {characterName}!", new FontData(Colors.Red, true, false));
                UIController.WriteToConsole($"Has ganado a {command.Arg2} puntos de experiencia.",
                    new FontData(Colors.Red, true, false));
                break;

            case Messages.UserKill:
                characterName = MapContainer.GetCharacter(command.Arg1).CharacterName ?? string.Empty;

                UIController.WriteToConsole($"¡{characterName} te ha matado!", new FontData(Colors.Red, true, false));
                break;


            case Messages.GoHome:
                int distance = command.Arg1;
                int time = command.Arg2;
                string home = command.StringArg1;

                var temp = time switch
                {
                    >= 60 when time % 60 == 0 => $"{time / 60} minutos.",
                    >= 60 => $"{time / 60} minutos y {time % 60} segundos.",
                    _ => $"{time} segundos."
                };

                UIController.WriteToConsole(
                    $"Te encuentras a {distance} mapas de la {home}, este viaje durará {temp}",
                    new FontData(Colors.Red, true, false));

                _gameContext.Traveling = true;
                break;

            case Messages.FinishHome:
                UIController.WriteToConsole("Has llegado a tu hogar. El viaje ha finalizado.",
                    new FontData(Colors.White, true, false));

                _gameContext.Traveling = false;
                break;

            case Messages.CancelGoHome:
                UIController.WriteToConsole("Tu viaje ha sido cancelado.",
                    new FontData(Colors.Red, false, false));

                _gameContext.Traveling = false;
                break;
        }
    }

    [Handler(ServerPacketId.ChangeInventorySlot)]
    private void HandleChangeInventorySlotCommand(ChangeInventorySlotCommand command)
    {
        ItemData item = new();
        item.Name = command.Name;
        item.Index = command.Index;
        item.Type = (ItemClass)command.Type;
        item.Price = command.Price;
        item.Icon = GameAssets.GetTextureFromGrhId(command.GrhIndex);

        item.MaxHit = command.MaxHit;
        item.MinHit = command.MinHit;

        item.MinDef = command.MinDef;
        item.MaxDef = command.MaxDef;

        var itemStack = new ItemStack(
            Quantity: command.Quantity,
            Equipped: command.Equipped,
            Item: item);
        
        _gameContext.PlayerInventory[command.Slot - 1] = itemStack;
    }

    [Handler(ServerPacketId.ChangeNPCInventorySlot)]
    private void HandleChangeNPCInventorySlot(ChangeNPCInventorySlotCommand command)
    {
        ItemData item = new();
        item.Name = command.Name;
        item.Index = command.Index;
        item.Type = (ItemClass)command.Type;
        item.Price = command.Price;
        item.Icon = GameAssets.GetTextureFromGrhId(command.GrhIndex);

        item.MaxHit = command.MaxHit;
        item.MinHit = command.MinHit;

        item.MinDef = command.MinDef;
        item.MaxDef = command.MaxDef;

        var itemStack = new ItemStack(
            Quantity: command.Quantity,
            Equipped: false,
            Item: item);
        
        _gameContext.TradeInventory[command.Slot - 1] = itemStack;
    }
    
    [Handler(ServerPacketId.ChangeSpellSlot)]
    private void HandleChangeSpellSlot(ChangeSpellSlotCommand command)
    {
        
    }

    [Handler(ServerPacketId.TradeOK)]
    private void HandleTradeOK()
    {
        
    }

    [Handler(ServerPacketId.CommerceInit)]
    private void HandleCommerceInit()
    {
        UIController.OpenTrade();
    }

    [Handler(ServerPacketId.CommerceEnd)]
    private void HandleCommerceEnd()
    {
        UIController.CloseTrade();
    }

    [Handler(ServerPacketId.ChangeBankSlot)]
    private void HandleChangeBankSlot(ChangeBankSlotCommand command)
    {
        ItemData item = new();
        item.Name = command.Name;
        item.Index = command.Index;
        item.Type = (ItemClass)command.Type;
        item.Price = command.Price;
        item.Icon = GameAssets.GetTextureFromGrhId(command.GrhIndex);

        item.MaxHit = command.MaxHit;
        item.MinHit = command.MinHit;

        item.MinDef = command.MinDef;
        item.MaxDef = command.MaxDef;

        var itemStack = new ItemStack(
            Quantity: command.Quantity,
            Equipped: false,
            Item: item);
        
        _gameContext.BankInventory[command.Slot - 1] = itemStack;
    }

    [Handler(ServerPacketId.BankInit)]
    private void HandleBankInit(BankInitCommand command)
    {
        UIController.OpenBank();
        UIController.UpdateBankGold(command.Gold);
    }

    [Handler(ServerPacketId.BankEnd)]
    private void HandleBankEnd()
    {
        UIController.CloseBank();
    }

    [Handler(ServerPacketId.BankOK)]
    private void HandleBankOk()
    {
        
    }

    [Handler(ServerPacketId.UpdateBankGold)]
    private void HandleUpdateBankGold(UpdateBankGoldCommand command)
    {
        UIController.UpdateBankGold(command.Gold);
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

    [Handler(ServerPacketId.PlayWave)]
    private void HandlePlayWave(PlayWaveCommand command)
    {
        AudioManager.Instance.PlayAudio(command.WaveId);
    }

    [Handler(ServerPacketId.AreaChanged)]
    private void HandleAreaChanged(AreaChangedCommand command)
    {
        
    }

    [Handler(ServerPacketId.ObjectCreate)]
    private void HandleObjectCreate(ObjectCreateCommand command)
    {
        MapContainer.RemoveObject(command.X, command.Y);
        MapContainer.AddObject(command.GrhIndex, command.X, command.Y);
    }

    [Handler(ServerPacketId.ObjectDelete)]
    private void HandleObjectDelete(ObjectDeleteCommand command)
    {
        MapContainer.RemoveObject(command.X, command.Y);;
    }

    [Handler(ServerPacketId.ErrorMsg)]
    private void HandleErrorMessage(ErrorMessageCommand command)
    {
        
    }

    [Handler(ServerPacketId.ConsoleMsg)]
    private void HandleConsoleMessage(ConsoleMessageCommand command)
    {
        UIController.WriteToConsole(command.Message, GameAssets.FontDataList[command.FontIndex]);   
    }

    [Handler(ServerPacketId.ShowMessageBox)]
    private void HandleShowMessageBox(ShowMessageBoxCommand command)
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
        MapContainer.RemoveCharacter(command.CharIndex);
        
        PlayerType privileges = (PlayerType)command.Privileges;
        
        if (privileges != 0)
        {
            if ((privileges & PlayerType.ChaosCouncil) != PlayerType.None &&
                (privileges & PlayerType.User) != PlayerType.None)
            {
                privileges = privileges ^ PlayerType.ChaosCouncil;
            }

            if ((privileges & PlayerType.RoyalCouncil) != PlayerType.None &&
                (privileges & PlayerType.User) != PlayerType.None)
            {
                privileges = privileges ^ PlayerType.RoyalCouncil;
            }

            if ((privileges & PlayerType.RoleMaster) != PlayerType.None)
            {
                privileges = PlayerType.RoleMaster;
            }

            privileges = (PlayerType)(Math.Log((double)privileges) / Math.Log(2));
        }

        command.Privileges = (int)privileges;
        
        CharacterController character = ResourceLoader
            .Load<PackedScene>("res://scenes/entities/character/character.tscn")
            .Instantiate<CharacterController>();
        
        character.CharacterName = command.Name;
        character.CharacterNameColor = Utils.GetNickColor(command.NickColor, command.Privileges);
        character.CharacterId = command.CharIndex;
        character.Privileges = command.Privileges;
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
            character.CharacterInvisible = !command.Invisible;
        }
    }

    [Handler(ServerPacketId.RainToggle)]
    private void HandleRainToggle()
    {
        
    }

    [Handler(ServerPacketId.ShowSignal)]
    private void HandleShowSignal(ShowSignalCommand command)
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

    [Handler(ServerPacketId.PosUpdate)]
    private void HandlePosUpdate(PosUpdateCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(_mainCharacterId);
        
        if (character != null)
        {
            character.StopMoving();
            character.GridPosition = new Vector2I(command.X, command.Y);
            character.Position =  new Vector2((command.X - 1) * 32, (command.Y - 1) * 32) + new Vector2(16, 32);
            
            //TODO Fix
            //_gameInput.minimap.update_player_position(p.x, p.y)
        }
    }

    [Handler(ServerPacketId.ForceCharMove)]
    private void HandleForceCharMove(ForceCharMoveCommand command)
    {
        CharacterController character = MapContainer.GetCharacter(_mainCharacterId);
        
        if (character != null)
        {
            character.StopMoving();
            MoveCharacter(_mainCharacterId, (Heading)command.Heading);
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
        _gameContext.PlayerStats.MinHp = command.MinHp;
        _gameContext.PlayerStats.MaxHp = command.MaxHp;
        
        _gameContext.PlayerStats.MinMp = command.MinMana;
        _gameContext.PlayerStats.MaxMp = command.MaxMana;
    }

    [Handler(ServerPacketId.UpdateHungerAndThirst)]
    private void HandleUpdateHungerAndThirstCommand(UpdateHungerAndThirstCommand command)
    {
        
    }

    [Handler(ServerPacketId.UpdateHP)]
    private void HandleUpdateHp(UpdateHpCommand command)
    {
        _gameContext.PlayerStats.MinHp = command.MinHp;
    }

    [Handler(ServerPacketId.UpdateMana)]
    private void HandleUpdateMp(UpdateManaCommand command)
    {
        _gameContext.PlayerStats.MinMp = command.MinMP;
    }

    [Handler(ServerPacketId.ChatOverHead)]
    private void HandleChatOverHead(ChatOverHeadCommand command)
    {
        MapContainer.GetCharacter(command.CharIndex)?
            .Talk(command.Message, command.Color);
    }

    [Handler(ServerPacketId.RemoveCharDialog)]
    private void HandleRemoveCharDialog(RemoveCharDialogCommand command)
    {
        MapContainer.GetCharacter(command.CharIndex)?
            .Talk(string.Empty, Colors.White);
    }

    [Handler(ServerPacketId.RemoveDialogs)]
    private void HandleRemoveDialogs()
    {
        
    }
    
    [Handler(ServerPacketId.UpdateExp)]  
    private void HandleUpdateExp(UpdateExpCommand command)
    {
        
    }
}