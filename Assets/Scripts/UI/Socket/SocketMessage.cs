using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class ByteArray
{
    MemoryStream memoryStream;
    BinaryReader br;
    BinaryWriter bw;

    public int readIndex;
    public int writeIndex;

    public ByteArray()
    {
        memoryStream = new MemoryStream();
        br = new BinaryReader(memoryStream);
        bw = new BinaryWriter(memoryStream);
    }

    public string ReadStr()
    {
        Seek(readIndex, SeekOrigin.Begin);
        string s = br.ReadString();
        //因为binaryWriter写字符串时会在字符串前面加一字节，存储字符串的长度
        int len = Encoding.UTF8.GetBytes(s).Length + 1;
        readIndex += len;
        return s;
    }

    public double ReadDouble()
    {
        Seek(readIndex, SeekOrigin.Begin);
        double s = br.ReadDouble();
        readIndex += 8;
        return s;
    }

    public float ReadFloat()
    {
        Seek(readIndex, SeekOrigin.Begin);
        float s = br.ReadSingle();
        readIndex += 4;
        return s;
    }


    public int ReadInt32()
    {
        Seek(readIndex, SeekOrigin.Begin);
        int s = br.ReadInt32();
        readIndex += 4;
        return s;
    }


    public short ReadShort()
    {
        Seek(readIndex, SeekOrigin.Begin);
        short s = br.ReadInt16();
        readIndex += 2;
        return s;
    }

    public bool ReadBoolean()
    {
        Seek(readIndex, SeekOrigin.Begin);
        bool b = br.ReadBoolean();
        readIndex += 1;
        return b;
    }


    public void Write(byte[] bytes)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(bytes);
        writeIndex += bytes.Length;
    }

    public void Write(string str)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        int len = Encoding.UTF8.GetBytes(str).Length+1;
        bw.Write(str);
        writeIndex += len;
    }

    public void Write(double s)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(s);
        writeIndex += 8;
    }

    public void Write(float s)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(s);
        writeIndex += 4;
    }

    public void Write(int s)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(s);
        writeIndex += 4;
    }


    public void Write(short s)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(s);
        writeIndex += 2;
    }

    public void Write(bool b)
    {
        Seek(writeIndex, SeekOrigin.Begin);
        bw.Write(b);
        writeIndex += 1;
    }


    public byte[] GetBytes()
    {
        return memoryStream.ToArray();
    }
    public int GetReadIndex()
    {
        return readIndex;
    }


    public long position { get { return memoryStream.Position; } }
    public long length { get { return memoryStream.Length; } }
    public void Seek(int offset, SeekOrigin origin)
    {
        memoryStream.Seek(offset, origin);
    }


    public void Destroy()
    {
        br.Close();
        bw.Close();
        memoryStream.Close();
        memoryStream.Dispose();
    }

}




//按照功能分大模块
public enum Module : int
{
    Login,
    Role,
}
//在大模块中进行小模块的分类
public enum SubModule : int
{
    Register,
    Login,
    Attack,
    Walk,
}
public class SocketMessage
{
    public Module module { get; set; }
    public SubModule subModule { get; set; }
    public string message { get; set; }
    public int length { get; set; }

    public SocketMessage(Module mod, SubModule sub, string str)
    {
        this.module = mod;
        this.subModule = sub;
        this.message = str;
        //消息协议 字符串长度 + 主模块长度 + 子模块长度 + 字符串长度 系统自动添加的存储字符串长度的字节
        length = 4 + 4 + 4 + Encoding.UTF8.GetBytes(str).Length + 1;
    }

}
