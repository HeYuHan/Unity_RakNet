using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NetworkStream
{
    public virtual int ReceiveBuffLength
    {
        get { return 1024 * 512; }
    }
    public virtual int SendBuffLength
    {
        get { return 1024 * 512; }
    }
    public byte[] ReceiveBuff;
    public byte[] SendBuff;

    public int ReceiveOffset;
    public int ReadOffset;

    protected int m_ReadPosition;
    protected int m_ReadEnd;

    public int SendOffset;

    protected int m_WritePosition;
    protected int m_WriteEnd;

    public IConnection connection;
    public Action m_OnMessage;

    public delegate bool BufferHandle(System.IO.Stream proto);


    public NetworkStream()
    {
        ReceiveBuff = new byte[ReceiveBuffLength];
        SendBuff = new byte[SendBuffLength];
    }
    public void Reset()
    {
        ReceiveOffset = 0;

        m_ReadPosition = 0;
        m_ReadEnd = 0;

        SendOffset = 0;

        m_WritePosition = 0;
        m_WriteEnd = 0;

        ReadOffset = 0;
    }
    public void CheckWrite(int len)
    {
        if (len + m_WriteEnd > SendBuffLength)
        {
            throw new Exception(string.Format("write data to big wirte end:{0} length:{1}", m_WriteEnd, SendBuffLength));
        }
    }
    public void CheckRead(int len)
    {
        if (len + m_ReadPosition > m_ReadEnd)
        {
            throw new Exception(string.Format("read data error pos:{0} length:{1} end:{2}", m_ReadPosition, len, m_ReadEnd));
        }
    }
    public void BeginWrite()
    {

        m_WritePosition = SendOffset;
        m_WriteEnd = m_WritePosition + connection.Type == ConnectionType.UDP ? 5 : 4;
    }
    public void EndWrite()
    {
        int head_len = connection.Type == ConnectionType.UDP ? 5 : 4;
        int len = m_WriteEnd - m_WritePosition - head_len;
        int head_index = m_WritePosition;
        if (connection.Type == ConnectionType.UDP)
        {
            SendBuff[head_index++] = 254;
        }
        SendBuff[head_index++] = (byte)((len >> 0) & 0xff);
        SendBuff[head_index++] = (byte)((len >> 8) & 0xff);
        SendBuff[head_index++] = (byte)((len >> 16) & 0xff);
        SendBuff[head_index++] = (byte)((len >> 24) & 0xff);
        if(connection.Type==ConnectionType.TCP)
        {

        }
        else
        {
            connection.Send(SendBuff, len + head_len);
            SendOffset = 0;
        }
        

    }
    public void WriteData(byte[] data, int index, int size)
    {
        CheckWrite(size);
        Array.Copy(data, index, SendBuff, m_WriteEnd, size);
        m_WriteEnd += size;
    }
    public void WriteByte(byte data)
    {
        CheckWrite(1);
        SendBuff[m_WriteEnd++] = data;
    }
    public void WriteInt(int data)
    {
        CheckWrite(4);
        SendBuff[m_WriteEnd] = (byte)((data >> 0) & 0xff);
        SendBuff[m_WriteEnd + 1] = (byte)((data >> 8) & 0xff);
        SendBuff[m_WriteEnd + 2] = (byte)((data >> 16) & 0xff);
        SendBuff[m_WriteEnd + 3] = (byte)((data >> 24) & 0xff);
        m_WriteEnd += 4;
    }
    public void WriteShort(short data)
    {
        CheckWrite(2);
        SendBuff[m_WriteEnd] = (byte)((data >> 0) & 0xff);
        SendBuff[m_WriteEnd + 1] = (byte)((data >> 8) & 0xff);
        m_WriteEnd += 2;
    }
    public void WriteFloat(float data)
    {
        CheckWrite(4);
        byte[] bs = BitConverter.GetBytes(data);
        Array.Copy(bs, 0, SendBuff, m_WriteEnd, bs.Length);
        m_WriteEnd += bs.Length;
        bs = null;
    }
    public void WriteVector3(Vector3 v3)
    {
        WriteFloat(v3.x);
        WriteFloat(v3.y);
        WriteFloat(v3.z);
    }
    public void WriteVector2(Vector2 v2)
    {
        WriteFloat(v2.x);
        WriteFloat(v2.y);
    }
    public void WriteVector4(Vector4 v4)
    {
        WriteFloat(v4.x);
        WriteFloat(v4.y);
        WriteFloat(v4.z);
        WriteFloat(v4.w);
    }
    public void WriteQuaternion(Quaternion rot)
    {
        WriteFloat(rot.x);
        WriteFloat(rot.y);
        WriteFloat(rot.z);
        WriteFloat(rot.w);
    }
    public void WriteString(string str)
    {
        byte[] bs = Encoding.UTF8.GetBytes(str);
        WriteInt(bs.Length);
        WriteData(bs, 0, bs.Length);
    }

    public byte ReadByte()
    {
        CheckRead(1);
        return ReceiveBuff[m_ReadPosition++];
    }
    public string ReadString()
    {
        int len = ReadInt();
        CheckRead(len);
        string ret = Encoding.UTF8.GetString(ReceiveBuff, m_ReadPosition, len);
        m_ReadPosition += len;
        return ret;
    }
    public float ReadFloat()
    {
        CheckRead(4);
        float ret = BitConverter.ToSingle(ReceiveBuff, m_ReadPosition);
        m_ReadPosition += 4;
        return ret;
    }
    public short ReadShort()
    {
        CheckRead(2);
        short ret = BitConverter.ToInt16(ReceiveBuff, m_ReadPosition);
        m_ReadPosition += 2;
        return ret;
    }
    public int ReadInt()
    {
        CheckRead(4);
        int ret = BitConverter.ToInt32(ReceiveBuff, m_ReadPosition);
        m_ReadPosition += 4;
        return ret;
    }




    public Vector2 ReadVector2()
    {
        return new Vector2() { x = ReadFloat(), y = ReadFloat() };
    }
    public Vector3 ReadVecotr3()
    {
        return new Vector3() { x = ReadFloat(), y = ReadFloat(), z = ReadFloat() };
    }
    public Vector4 ReadVector4()
    {
        return new Vector4() { x = ReadFloat(), y = ReadFloat(), z = ReadFloat(), w = ReadFloat() };
    }
    public Quaternion ReadQuaternion()
    {
        return new Quaternion(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
    }

    protected virtual void OnMessage()
    {
        if (m_OnMessage != null) m_OnMessage();
    }

    public void OnParseMessage()
    {
        int revc_size = connection.Read(ReceiveBuff, ReadOffset, ReceiveBuffLength - ReadOffset);
        if (revc_size <= 0) return;
        ReadOffset += revc_size;
        int size = ReadOffset - m_ReadPosition;
        while (size > 0)
        {
            //小于包头
            if (size < 4) break;
            int data_len = BitConverter.ToInt32(ReceiveBuff, m_ReadPosition);
            //数据未收完
            if (size - 4 < data_len || data_len < 0) break;
            m_ReadPosition += 4;
            m_ReadEnd = m_ReadPosition + data_len;
            OnMessage();
            m_ReadPosition = m_ReadEnd;
            size = ReadOffset - m_ReadPosition;
        }
        size = ReadOffset - m_ReadPosition;
        //移动未读完的数据
        if (m_ReadPosition > 0 && size > 0)
        {
            Array.Copy(ReceiveBuff, m_ReadPosition, ReceiveBuff, 0, size);

        }
        ReadOffset = size;
        m_ReadPosition = m_ReadEnd = 0;

    }
}