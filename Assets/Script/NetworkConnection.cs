using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RakNet;
public enum ConnectionType
{
    TCP = 0, UDP = 1
}
public interface IConnection
{
    int Read(byte[] data, int offset, int size);
    int Send(byte[] data, int offset, int size);
    void Send(byte[] data, int size);
    ConnectionType Type { get; }
}

public class UdpConnection : IConnection
{
    RakPeerInterface m_Socket;
    SystemAddress m_SystemAddress;
    Packet m_MessagePacket;
    public NetworkStream m_Stream;
    public Action m_OnConnected;
    public Action m_OnDisconnected;

    ConnectionType IConnection.Type
    {
        get
        {
            return ConnectionType.UDP;
        }
    }

    byte GetPacketIdentifier(Packet p)
    {
        if (p == null)
            return 255;

        if (p.data[0] == (byte)DefaultMessageIDTypes.ID_TIMESTAMP)
        {
            return p.data[sizeof(byte) + sizeof(ulong)];
        }
        else
            return p.data[0];
    }
    protected void OnConnected()
    {
        if (m_OnConnected != null) m_OnConnected();
    }
    protected void OnDisconnected()
    {
        if (m_OnDisconnected != null) m_OnDisconnected();
    }
    public void Update()
    {
        if (null == m_Socket) return;
        for (m_MessagePacket = m_Socket.Receive(); null != m_MessagePacket; m_Socket.DeallocatePacket(m_MessagePacket), m_MessagePacket = m_Socket.Receive())
        {
            // We got a packet, get the identifier with our handy function
            DefaultMessageIDTypes packetIdentifier = (DefaultMessageIDTypes)GetPacketIdentifier(m_MessagePacket);

            // Check if this is a network message packet
            switch (packetIdentifier)
            {
                case DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION:
                    // Connection lost normally
                    //printf("ID_DISCONNECTION_NOTIFICATION\n");
                    OnDisconnected();
                    break;
                case DefaultMessageIDTypes.ID_ALREADY_CONNECTED:
                    // Connection lost normally
                    //printf("ID_ALREADY_CONNECTED with guid %" PRINTF_64_BIT_MODIFIER "u\n", m_MessagePacket->guid);
                    break;
                case DefaultMessageIDTypes.ID_INCOMPATIBLE_PROTOCOL_VERSION:
                    //printf("ID_INCOMPATIBLE_PROTOCOL_VERSION\n");
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_DISCONNECTION_NOTIFICATION: // Server telling the clients of another client disconnecting gracefully.  You can manually broadcast this in a peer to peer enviroment if you want.
                    //printf("ID_REMOTE_DISCONNECTION_NOTIFICATION\n");
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_CONNECTION_LOST: // Server telling the clients of another client disconnecting forcefully.  You can manually broadcast this in a peer to peer enviroment if you want.
                    //printf("ID_REMOTE_CONNECTION_LOST\n");
                    OnDisconnected();
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_NEW_INCOMING_CONNECTION: // Server telling the clients of another client connecting.  You can manually broadcast this in a peer to peer enviroment if you want.
                    //printf("ID_REMOTE_NEW_INCOMING_CONNECTION\n");
                    break;
                case DefaultMessageIDTypes.ID_CONNECTION_BANNED: // Banned from this server
                    //printf("We are banned from this server.\n");
                    break;
                case DefaultMessageIDTypes.ID_CONNECTION_ATTEMPT_FAILED:
                    //printf("Connection attempt failed\n");
                    OnDisconnected();
                    break;
                case DefaultMessageIDTypes.ID_NO_FREE_INCOMING_CONNECTIONS:
                    // Sorry, the server is full.  I don't do anything here but
                    // A real app should tell the user
                    //printf("ID_NO_FREE_INCOMING_CONNECTIONS\n");
                    OnDisconnected();
                    break;

                case DefaultMessageIDTypes.ID_INVALID_PASSWORD:
                    //printf("ID_INVALID_PASSWORD\n");
                    OnDisconnected();
                    break;

                case DefaultMessageIDTypes.ID_CONNECTION_LOST:
                    // Couldn't deliver a reliable packet - i.e. the other system was abnormally
                    // terminated
                    //printf("ID_CONNECTION_LOST\n");
                    OnDisconnected();
                    break;

                case DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED:
                    // This tells the client they have connected
                    //printf("ID_CONNECTION_REQUEST_ACCEPTED to %s with GUID %s\n", m_MessagePacket->systemAddress.ToString(true), m_MessagePacket->guid.ToString());
                    //printf("My external address is %s\n", m_Socket->GetExternalID(m_MessagePacket->systemAddress).ToString(true));
                    m_SystemAddress = m_MessagePacket.systemAddress;
                    OnConnected();
                    break;
                case DefaultMessageIDTypes.ID_CONNECTED_PING:
                case DefaultMessageIDTypes.ID_UNCONNECTED_PING:
                    //printf("Ping from %s\n", m_MessagePacket->systemAddress.ToString(true));
                    break;
                default:
                    // It's a client, so just show the message
                    //printf("%s\n", p->data);
                    if (null != m_Stream) m_Stream.OnParseMessage();
                    break;
            }
        }
    }
    public int Read(byte[] data, int offset, int size)
    {
        if (null != m_MessagePacket)
        {
            if (size < m_MessagePacket.length)
            {
                throw new Exception("data size too less");
            }
            Array.Copy(m_MessagePacket.data, 0, data, offset, m_MessagePacket.length);
            return (int)m_MessagePacket.length;
        }
        return 0;
    }
    public void Send(byte[] data, int size)
    {
        uint ret = m_Socket.Send(data,size, PacketPriority.HIGH_PRIORITY, PacketReliability.RELIABLE_ORDERED, '0', m_SystemAddress, false);
        Debug.Log(ret);
    }



    public bool Connect(string ip, int port, string pwd)
    {

        m_Socket = RakPeerInterface.GetInstance();
        m_Socket.AllowConnectionResponseIPMigration(false);
        SocketDescriptor socketDescriptor = new SocketDescriptor(0, "0");
        socketDescriptor.socketFamily = 2;
        m_Socket.Startup(8, socketDescriptor, 1);
        m_Socket.SetOccasionalPing(true);
        ConnectionAttemptResult car = m_Socket.Connect(ip, (ushort)port, pwd, pwd.Length);
        if (car == ConnectionAttemptResult.CONNECTION_ATTEMPT_STARTED)
        {
            return true;
        }
        else
        {
            m_Socket.Shutdown(300);
            RakPeerInterface.DestroyInstance(m_Socket);
            return false;
        }

    }




    public void DisConnect()
    {
        m_Socket.CloseConnection(m_SystemAddress, false);
        m_Socket.Shutdown(300);
        RakPeerInterface.DestroyInstance(m_Socket);
        m_SystemAddress = null;
        m_Socket = null;
        OnDisconnected();

    }

    public int Send(byte[] data, int offset, int size)
    {
        throw new NotImplementedException();
    }

}
