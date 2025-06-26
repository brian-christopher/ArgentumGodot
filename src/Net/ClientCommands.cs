namespace ArgentumOnline.Net;

public static class ClientCommands
{
    private static BinaryPacketWriter WriteByte(this BinaryPacketWriter binaryPacketWriter, ClientPacketId packetId)
    {
        return binaryPacketWriter.WriteByte((byte)packetId);
    }
    
    private static void SendOnePacket(Core.AutoLoads.NetworkClient client, ClientPacketId packetId)
    {
        client.Send([(byte)packetId]);
    }
    
    public static void SendLoginExistingCharacter(this Core.AutoLoads.NetworkClient client, string username, string password)
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

    public static void SendLoginNewChar(this Core.AutoLoads.NetworkClient client, string username, string password, string email,
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

    public static void SendThrowDice(this Core.AutoLoads.NetworkClient client)
    {
        SendOnePacket(client, ClientPacketId.ThrowDices);
    }
}