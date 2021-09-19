using LiteNetLib;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Newtonsoft.Json;
//for Dictionary
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour, INetEventListener
{
    [SerializeField] private ServerController serverController;

    public const short PORT = 9050;
    public const string KEY = "MYKEY";
    private NetManager server;
    private int clientCurrnetId = 0;
    private Dictionary<int, PeerConnection> peerConnections = new Dictionary<int, PeerConnection>();
    private ModelToObjectMapper modelToObjectMapper;

    private void Awake()
    {
        Debug.Log("Awake");
        server = new NetManager(this);
        server.Start(PORT);
    }
    private void Start()
    {
        modelToObjectMapper = new ModelToObjectMapper(serverController);
    }

    private void Update()
    {
        server.PollEvents();
    }
    public void SendAll(BaseModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        foreach (var clientConnection in peerConnections.Values)
        {
            clientConnection.Send(json);
        }
    }
    public void OnConnectionRequest(ConnectionRequest request)
    {
        Debug.Log("OnConnectionRequest");
        CreatePeerConnection(request.AcceptIfKey(KEY));
    }
    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) => Debug.Log("OnNetworkError");
    public void OnNetworkLatencyUpdate(NetPeer peer, int latency) { }
    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        Debug.Log("OnNetworkReceive");
        modelToObjectMapper.DeserializeToFunction(peer, reader.GetString());
    }
    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType) => Debug.Log("OnNetworkReceiveUnconnected");
    private void CreatePeerConnection(NetPeer peer)
    {
        var id = clientCurrnetId++;
        var peerConnection = new PeerConnection(this, peer, id);
        peer.Tag = peerConnection;

        peerConnections.Add(id, peerConnection);
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("OnPeerConnected");
    }
    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) => Debug.Log("OnPeerDisconnected");

    public void Remove(PeerConnection peerConnection)
    {
        peerConnections.Remove(peerConnection.Id);
    }
}