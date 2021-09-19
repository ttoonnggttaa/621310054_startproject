using LiteNetLib;
using LiteNetLib.Utils;
using System;
using Newtonsoft.Json;

public class PeerConnection
{
    private Server server;
    private NetPeer peer;
    private int id;
    public int Id => id;

    public PeerConnection(Server server, NetPeer peer, int id)
    {
        this.server = server;
        this.peer = peer;
        this.id = id;
    }

    public void Send(BaseModel model) => Send(JsonConvert.SerializeObject(model));

    public void Send(string json) => peer.Send(NetDataWriter.FromString(json), DeliveryMethod.ReliableOrdered);

    public void Disconnected()
    {
        server.Remove(this);
    }
}
