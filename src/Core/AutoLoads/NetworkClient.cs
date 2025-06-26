using System;
using System.Diagnostics;
using System.Linq;
using Godot;
using Array = Godot.Collections.Array;

namespace ArgentumOnline.Core.AutoLoads;

public partial class NetworkClient : Node
{
    private readonly StreamPeerTcp _socket = new();
    private StreamPeerTcp.Status _status = StreamPeerTcp.Status.None;
    
    public event Action OnConnect;
    public event Action OnDisconnect;
    public event Action<byte[]> OnReceive;

    public static NetworkClient Instance { get; private set; }
    
    public NetworkClient()
    {
        Debug.Assert(Instance == null, "Only one instance of GameNetworkClient is allowed.");
        Instance = this;
        
        SetProcess(false);
    }

    public void ConnectToHost(string host, int port)
    {
        _status = StreamPeerTcp.Status.None;
        SetProcess(_socket.ConnectToHost(host, port) == Error.Ok);
    }

    public void DisconnectFromHost()
    {
        _socket.DisconnectFromHost();
    }

    public void Send(byte[] data)
    {
        if (data is not null && data.Any())
        {
            if (_socket.GetStatus() == StreamPeerTcp.Status.Connected)
            {
                _socket.PutData(data);
            }
        }
    }

    public override void _Process(double delta)
    {
        _socket.Poll();
        
        StreamPeerTcp.Status newStatus = _socket.GetStatus();
        
        if (newStatus != _status)
        {
            _status = newStatus;

            switch (_status)
            {
                case StreamPeerTcp.Status.None:
                    OnDisconnect?.Invoke();
                    break;
                case StreamPeerTcp.Status.Connecting:
                    break;
                case StreamPeerTcp.Status.Connected:
                    OnConnect?.Invoke();
                    break;
                case StreamPeerTcp.Status.Error:
                    OnDisconnect?.Invoke();
                    break;
            }
        }

        if (_status == StreamPeerTcp.Status.Connected)
        {
            int availableBytes = _socket.GetAvailableBytes();
            
            if (availableBytes > 0)
            {
                Array response = _socket.GetData(availableBytes);
                
                Error responseState = (Error)response[0].AsInt32();
                byte[] data = response[1].AsByteArray();

                if (responseState == Error.Ok)
                {
                    OnReceive?.Invoke(data);
                }
                else
                {
                    OnDisconnect?.Invoke();
                    SetProcess(false);
                }
            }
        }
    }
}