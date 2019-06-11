using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStream
{
    private byte[] m_data = new byte[1024];
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
}
