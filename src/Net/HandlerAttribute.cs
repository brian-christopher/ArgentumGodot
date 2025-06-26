using System;

namespace ArgentumOnline.Net;

[AttributeUsage(AttributeTargets.Method)]
public class HandlerAttribute(ServerPacketId packetId) : Attribute
{
    public ServerPacketId PacketId { get; } = packetId;
}