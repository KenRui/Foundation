using UnityEngine;
using UnityEngine.Networking;

public class NetworkRoot : MonoBehaviour
{


    public void StartServer()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientconnect);
        NetworkServer.RegisterHandler(MsgType.Error, (netMsg) =>
        {
            Debug.Log("onError:" + netMsg.msgType);
        });
        bool ret = NetworkServer.Listen(20086);
        if (ret)
        {
            Debug.Log("Server started...");
        }
        else
        {
            Debug.Log("Server start error...");
        }
    }

    public void OnClientconnect(NetworkMessage netMsg)
    {
        var connet = netMsg.conn;
        Debug.Log("Client Connect CLientIP:" + connet.address);
    }
    

    public void StartClient()
    {
        var client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, (netMsg) =>
        {
            Debug.Log("connet server.. ");
        });
        client.Connect("127.0.0.1", 20086);
    }
}
