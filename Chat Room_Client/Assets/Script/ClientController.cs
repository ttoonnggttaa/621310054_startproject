using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClientController : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private Text textbox;
    [SerializeField] private InputField inputMsgField;
    [SerializeField] private InputField inputNameField;

    public void StartChat()
    {
        char c = (char)('A' + Random.Range(0, 26));
        inputNameField.text = "MR." + c;
        textbox.text = "Connecting to Server...";
    }

    public void ShowConected()
    {
        textbox.text += "\n" + "OnPeerConnected";
    }

    public void ShowUnConected()
    {
        textbox.text += "\n" + "UnConnected, Server is not respond.";
    }

    public void DisplayMsg(UpdateMessageModel msg)
    {
        textbox.text += "\n" + msg.messageData;
    }

    public void sendMsg()
    {
        client.Send(new MessageModel
        {
            sender = inputNameField.text,
            message = inputMsgField.text
        }
        );
        Debug.Log("Send");
    }

}
