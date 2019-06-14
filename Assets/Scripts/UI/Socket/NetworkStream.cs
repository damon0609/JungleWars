using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
public class NetworkStream
{
    private byte[] m_data;
    public byte[] data
    {
        get { return m_data; }
    }
    public int startIndex = 0;
    public int remainIndex
    {
        get { return m_data.Length - startIndex; }
    }

    public void ReadData(int count)
    {
        startIndex += count;
        if (startIndex < 4) return;
    }

    public NetworkStream(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        byte[] lenBytes = BitConverter.GetBytes(bytes.Length);

        m_data = new byte[bytes.Length+lenBytes.Length];
        lenBytes.CopyTo(m_data,0);
        bytes.CopyTo(m_data, lenBytes.Length);
    }
}
