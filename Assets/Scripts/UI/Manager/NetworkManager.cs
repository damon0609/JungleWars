using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    private Client client;
    protected override void Init()
    {

        client = new Client("192.168.1.104", 6688);
        client.Init();
    }

    public void SendAsync(string msg)
    {
        client.SendData(msg);
    }

    public void SendAsync(byte[] bytes)
    {
        client.SendData(bytes);
    }

    public void SendAsync(NetworkStream stream)
    {
        client.SendData(stream);
    }

    public void Close()
    {
        client.Close();
    }
}
