using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>
{
    public Client client;
    protected override void Init()
    {

        client = new Client("192.168.1.101", 6688);
        client.Init();
    }

    public void SendAsync<T>(T data,MainCommand mainCommand,SubCommand subCommand)
    {
        client.SendData<T>(data,mainCommand,subCommand);
    }


    public void SendAsync(SocketMessage stream)
    {
        client.SendData(stream);
    }

    public void SendAsync(NetPacket stream)
    {
        client.SendData(stream);
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
