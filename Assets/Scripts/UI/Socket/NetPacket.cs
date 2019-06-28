using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public enum CommandType:int
{
    Login=1001,
    Register,
}

public enum SubCommandType
{

}
public class NetPacket
{
    public byte[] datas;

    private int m_index = 0;
    public int index
    {
        get { return m_index; }
        set { m_index = value; }
    }

    public int length
    {
        get { return datas.Length; }
    }

    public void CopyTo(byte[] bytes, int length)
    {
        datas = new byte[length];
        bytes.CopyTo(datas, 0);
        Console.WriteLine("接受到的实际数据" + length);
    }

    public NetPacket(int crocode, long sessionID, int command, int subCommand, int encrypt, string[] messages)
    {
        BuildData(crocode, sessionID, command, subCommand, encrypt, messages);
    }

    //构造数据包
    //消息校验码-身份id-主命令-子命令-加密方式-消息内容
    public void BuildData(int crocode, long sessionID, int command, int subCommand, int encrypt, string[] messages)
    {
        crocode = 65433;

        #region 消息体部分解析
        int messageBodyLen = 0;
        for (int i = 0; i < messages.Length; i++)
        {
            string str = messages[i];
            if (str == "")
                continue;
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            messageBodyLen += bytes.Length;
        }
        messageBodyLen += messages.Length * 4;
        byte[] messageByte = new byte[messageBodyLen];
        int index = 0;
        for (int i = 0; i < messages.Length; i++)
        {
            string str = messages[i];
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] len = BitConverter.GetBytes(bytes.Length);
            len.CopyTo(messageByte, index);
            index += 4;
            bytes.CopyTo(messageByte, index);
            index += bytes.Length;
        }
        #endregion

        #region 消息头部分
        byte[] crocodeByte = BitConverter.GetBytes(crocode);
        byte[] messageLenByte = BitConverter.GetBytes(messageByte.Length);
        byte[] sessionIDByte = BitConverter.GetBytes(sessionID);
        byte[] commadByte = BitConverter.GetBytes(command);
        byte[] subCommandByte = BitConverter.GetBytes(subCommand);
        byte[] encryptByte = BitConverter.GetBytes(encrypt);

        datas = new byte[crocodeByte.Length + messageLenByte.Length + sessionIDByte.Length
                                + commadByte.Length + subCommandByte.Length
                                + encryptByte.Length + messageByte.Length];

        crocodeByte.CopyTo(datas, 0);
        messageLenByte.CopyTo(datas, 4);
        sessionIDByte.CopyTo(datas, 8);
        commadByte.CopyTo(datas, 16);
        subCommandByte.CopyTo(datas, 20);
        encryptByte.CopyTo(datas, 24);
        messageByte.CopyTo(datas, 28);

        #endregion
    }

    public void ReadData()
    {
        if (datas == null || datas.Length == 0)
            return;

        int crocode = BitConverter.ToInt32(datas, 0);
        Debug.Log("消息校验码:" + crocode);

        int messageLen = BitConverter.ToInt32(datas, 4);
        Debug.Log("消息总长度:" + messageLen);

        int sessionID = BitConverter.ToInt32(datas, 8);
        Debug.Log("身份ID:" + sessionID);

        int command = BitConverter.ToInt32(datas, 16);
        Debug.Log("主命令:" + command);

        int subCommand = BitConverter.ToInt32(datas, 20);
        Debug.Log("子命令:" + subCommand);

        int encrypt = BitConverter.ToInt32(datas, 24);
        Debug.Log("加密方式:" + encrypt);

        byte[] temp = new byte[messageLen];
        Array.Copy(datas, 28, temp, 0, temp.Length);
        int index = 0;
        while (index < messageLen)
        {
            int strLen = BitConverter.ToInt32(temp, index);
            index += 4;
            byte[] message = new byte[strLen];
            Array.Copy(temp, index, message, 0, strLen);
            string str = Encoding.UTF8.GetString(message);
            Debug.Log("字符串长度:" + strLen + "--" + str);
            index += strLen;
        }

    }
}