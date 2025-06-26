using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgentumOnline.Net.Commands;
using Godot;

namespace ArgentumOnline.Net;

public sealed class CommandDispatcher
{
    private readonly object _instance;
    private Dictionary<ServerPacketId, MethodInfo> _methodInfos;

    public CommandDispatcher(object instance)
    {
        _instance = instance;
        _methodInfos = _instance.GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<HandlerAttribute>() != null) 
            .ToDictionary(m => m.GetCustomAttribute<HandlerAttribute>()!.PacketId, m => m);  
    }

    public void Dispatch(Reader reader)
    { 
        ServerPacketId packetId = (ServerPacketId)reader.ReadByte();
        
        if (_methodInfos.TryGetValue(packetId, out MethodInfo methodInfo))
        {
            ParameterInfo[] parameters = methodInfo.GetParameters(); 
            if (parameters.Any())
            {
                ICommand message = (ICommand)Activator.CreateInstance(parameters[0].ParameterType);
                message!.Unpack(reader);
                methodInfo.Invoke(_instance, [message]);
            }
            else
            {
                methodInfo.Invoke(_instance, []);
            }
        }
        else
        {
            GD.PrintErr($"packet was not found: {packetId}");
        }
    }
}