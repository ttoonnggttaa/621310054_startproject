using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Newtonsoft.Json;


public class Client : MonoBehaviour, INetEventListener
{
    [SerializeField] private ClientController clientController;

    private NetManager client;
    private NetPeer peer;
    private string host = "localhost";
    private short port = 9050;
    private ModelToObjectMapper modelToObjectMapper;
    private void Awake()
    {
        Debug.Log("Awake");
        client = new NetManager(this);
        client.Start();
        client.Connect(host, port, "MYKEY");
    }
    private void Start()
    {
        clientController.StartChat();
        modelToObjectMapper = new ModelToObjectMapper(clientController);
    }
    private void Update()
    {
        client.PollEvents();
    }
    public void Send(BaseModel baseModel)
    {
        var json = JsonConvert.SerializeObject(baseModel);
        var netDataWriter = NetDataWriter.FromString(json);
        peer?.Send(netDataWriter, DeliveryMethod.ReliableOrdered);
    }
    public void OnConnectionRequest(ConnectionRequest request) => Debug.Log("OnConnectionRequest");
    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) => Debug.Log("OnNetworkError");
    public void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }
    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        modelToObjectMapper.DeserializeToFunction(reader.GetString());
    }
    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType) => Debug.Log("OnNetworkReceiveUnconnected");
    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("OnPeerConnected");
        this.peer = peer;
        clientController.ShowConected();
    }
    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("OnPeerDisconnected");
        clientController.ShowUnConected();
    }
}