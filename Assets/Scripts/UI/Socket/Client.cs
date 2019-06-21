using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

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

    public void SendData(byte[] bytes)
    {
        if (m_Connected)
        {
            m_Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, ar => {
                try
                {
                    int length = m_Socket.EndSend(ar);
                    if (length == 0)
                    {
                        Debug.Log("发送的数据字节长度为0");
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

    public void Close()
    {
        Debug.Log("关闭客户端连接");
        m_Connected = false;
        m_Socket.Shutdown(SocketShutdown.Both);
        m_Socket.Close();
    }
}
