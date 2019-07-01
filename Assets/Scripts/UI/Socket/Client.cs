using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using ProtoBuf;
using System.IO;

public enum MainCommand:int
{
    Login=1000,
    CreateRole,
    Attack,
}

public enum SubCommand:int
{
    None=1000,
    Login,
    Register,
    
}

public class Client
{
    private IPAddress m_IpAddress;
    public IPAddress iPAddress
    {
        get { return m_IpAddress; }
    }

    private int m_Port;
    public int port
    {
        get { return m_Port; }
    }

    private Socket m_Socket;
    public Socket socket
    {
        get { return m_Socket; }
    }

    private byte[] bytes = new byte[1024];

    private bool m_Connected = false;
    public bool connected
    {
        get { return m_Connected; }
    }

    public Client(string ipAddress, int port)
    {
        this.m_IpAddress = IPAddress.Parse(ipAddress);
        this.m_Port = port;
    }

    public void Init()
    {
        IPEndPoint ipe = new IPEndPoint(m_IpAddress,m_Port);
        m_Socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        m_Socket.Connect(ipe);
        m_Connected = true;
        BeginReceive();
    }

    //接受到服务器数据
    private void BeginReceive()
    {
        m_Socket.BeginReceive(bytes,0,bytes.Length,SocketFlags.None,ar=> {
            try
            {
                if (!m_Connected || !m_Socket.Connected) return;//需要放置在异步结束时进行判断
                int length = m_Socket.EndReceive(ar);
                if (length == 0)
                {
                    m_Connected = false;
                    Close();
                }
                string msg = Encoding.UTF8.GetString(bytes,0,length);
                BeginReceive();
            }
            catch (Exception e)
            {
                m_Connected = false;
                Close();
            }
        },null);
    }

    //向服务器发送数据
    public void SendData(string msg)
    {
        byte[] strBytes = Encoding.UTF8.GetBytes(msg);
        Debug.Log("文本长度:"+strBytes.Length);
        byte[] lenBytes = BitConverter.GetBytes(strBytes.Length);
        Debug.Log("记录文本长度的长度:" + lenBytes.Length);

        byte[] bytes = new byte[lenBytes.Length+strBytes.Length];

        lenBytes.CopyTo(bytes,0);
        strBytes.CopyTo(bytes,lenBytes.Length);
        SendData(bytes);
    }
    public void SendData(SocketMessage message)
    {
        ByteArray byteArray = new ByteArray();
        byteArray.Write(message.length);//向数组中写数据总长度
        byteArray.Write((int)message.module);//向数组中写大模块分类
        byteArray.Write((int)message.subModule);//向字节数组中写入小模块分类
        byteArray.Write(message.message);//向字节数组中写入字符串消息

        SendData(byteArray.GetBytes());
        Debug.Log("长度:"+byteArray.ReadInt32()+"-"+(Module)byteArray.ReadInt32()+"-"+(SubModule)byteArray.ReadInt32()+"-"+byteArray.ReadStr());
    }
    public void SendData(byte[] bytes)
    {
        if (m_Connected)
        {
            m_Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, ar => {
                try
                {
                    int length = m_Socket.EndSend(ar);
                    if (length == 0)
                        Debug.Log("发送的数据字节长度为0");
                    else
                    {
                        Debug.Log("发送字节长度:"+length);
                    }
                }
                catch (Exception e)
                {
                    Close();
                }
            }, null);
        }
    }
    public void SendData(NetworkStream stream)
    {
        SendData(stream.data);
    }
    public void SendData(NetPacket stream)
    {
        SendData(stream.datas);
    }

    public void SendData<T>(T data,MainCommand mainCommand,SubCommand subCommand)
    {
        byte[] bytes = null;
        using (MemoryStream stream = new MemoryStream())
        {
            Serializer.Serialize<T>(stream,data);
            byte[] messageBytes = stream.ToArray();

            //消息头协议 积累消息长度+主命令+子命名+消息
            bytes = new byte[12 + messageBytes.Length];
            byte[] messageLenBytes = BitConverter.GetBytes(messageBytes.Length);
            Array.Copy(messageLenBytes,0,bytes,0,4);
            Array.Copy(messageBytes,0,bytes,12,messageBytes.Length);
        }
        byte[] mainBytes = BitConverter.GetBytes((int)mainCommand);
        Array.Copy(mainBytes, 0, bytes, 4, 4);

        byte[] subBytes = BitConverter.GetBytes((int)subCommand);
        Array.Copy(mainBytes, 0, bytes, 8, 4);

        SendData(bytes);
    }




    public void Close()
    {
        Debug.Log("关闭客户端连接");
        m_Connected = false;
        m_Socket.Shutdown(SocketShutdown.Both);
        m_Socket.Close();
    }
}
