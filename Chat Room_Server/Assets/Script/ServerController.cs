using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ServerController : MonoBehaviour
{
    [SerializeField] private Server server;

    public void updateMsgToAllClient(MessageModel messageModel)
    {
        string msg = messageModel.sender + " : " + messageModel.message;
        server.SendAll(new UpdateMessageModel { messageData = msg });
    }

}