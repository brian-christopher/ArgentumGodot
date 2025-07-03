using System.Runtime.CompilerServices;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Types;

namespace ArgentumOnline.Net;

public static class ClientCommands
{
    private static BinaryPacketWriter WriteByte(this BinaryPacketWriter binaryPacketWriter, ClientPacketId packetId)
    {
        return binaryPacketWriter.WriteByte((byte)packetId);
    }
    
    private static void SendOnePacket(NetworkClient client, ClientPacketId packetId)
    {
        client.Send([(byte)packetId]);
    }
    
    public static void SendLoginExistingCharacter(this NetworkClient client, string username, string password)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.LoginExistingChar)
            .WriteString(username)
            .WriteString(password)
            .WriteByte(0)
            .WriteByte(13)
            .WriteByte(0)
            .Build();
        
        client.Send(data);
    }

    public static void SendLoginNewChar(this NetworkClient client, string username, string password, string email,
        int @class, int race, int gender, int home, int head)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.LoginNewChar)
            .WriteString(username)
            .WriteString(password)
            .WriteByte(0)
            .WriteByte(13)
            .WriteByte(0)
            .WriteByte((byte)race)
            .WriteByte((byte)gender)
            .WriteByte((byte)@class)
            .WriteInteger((short)head)
            .WriteString(email)
            .WriteByte((byte)home)
            .Build();
        
        client.Send(data);
    }

    public static void SendThrowDice(this NetworkClient client)
    {
        SendOnePacket(client, ClientPacketId.ThrowDices);
    }

    public static void SendCommerceEnd(this NetworkClient client)
    {
        SendOnePacket(client, ClientPacketId.CommerceEnd);
    }

    public static void SendCommerceBuy(this NetworkClient client, int slot, int quantity)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.CommerceBuy)
            .WriteByte((byte)slot)
            .WriteInteger((short)quantity)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendCommerceSell(this NetworkClient client, int slot, int quantity)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.CommerceSell)
            .WriteByte((byte)slot)
            .WriteInteger((short)quantity)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendBankDepositItem(this NetworkClient client, int slot, int quantity)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.BankDeposit)
            .WriteByte((byte)slot)
            .WriteInteger((short)quantity)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendBankExtractItem(this NetworkClient client, int slot, int quantity)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.BankExtractItem)
            .WriteByte((byte)slot)
            .WriteInteger((short)quantity)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendBankEnd(this NetworkClient client)
    {
        SendOnePacket(client, ClientPacketId.BankEnd);
    }
    
    public static void SendWalk(this NetworkClient client, Heading heading)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.Walk)
            .WriteByte((byte)heading)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendChangeHeading(this NetworkClient client, Heading heading)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.ChangeHeading)
            .WriteByte((byte)heading)
            .Build();
        
        client.Send(data);
    }

    public static void SendDoubleClick(this NetworkClient client, int x, int y)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.DoubleClick)
            .WriteByte((byte)x)
            .WriteByte((byte)y)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendLeftClick(this NetworkClient client, int x, int y)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.LeftClick)
            .WriteByte((byte)x)
            .WriteByte((byte)y)
            .Build();
        
        client.Send(data);
    }
    
    public static void SendWorkLeftClick(this NetworkClient client, int x, int y, int skill)
    {
        byte[] data = new BinaryPacketWriter()
            .WriteByte(ClientPacketId.WorkLeftClick)
            .WriteByte((byte)x)
            .WriteByte((byte)y)
            .WriteByte((byte)skill)
            .Build();
        
        client.Send(data);
    }
}