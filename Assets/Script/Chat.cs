using UnityEngine;
using System.Collections;
using RakNet;
using System.Net.Sockets;
using System;

public class Chat : MonoBehaviour {

    RakNetStatistics rss;
    RakPeerInterface client;
    Packet p;
    SystemAddress clientID;
    SocketDescriptor socketDescriptor;
    // Use this for initialization
    void Start () {
        client = RakPeerInterface.GetInstance();
        client.AllowConnectionResponseIPMigration(false);
        socketDescriptor = new SocketDescriptor(Convert.ToUInt16(0), "0");
        socketDescriptor.socketFamily = 2;
        client.Startup(8, socketDescriptor, 1);
        client.SetOccasionalPing(true);
        for(uint i=0;i<client.GetNumberOfAddresses();i++)
        {
            Debug.Log(string.Format("{0} {1}", i + 1, client.GetLocalIP(i)));
        }
        ConnectionAttemptResult b = client.Connect("127.0.0.1", 1234, "Rumpelstiltskin", "Rumpelstiltskin".Length);
        Debug.Log("Connect Result:" + b);
    }
    private void OnApplicationQuit()
    {
        client.Shutdown(300);

        // We're done with the network
        RakPeerInterface.DestroyInstance(client);
    }
    // Update is called once per frame
    void Update () {
        for (p = client.Receive(); p!=null; client.DeallocatePacket(p), p = client.Receive())
        {
            // We got a packet, get the identifier with our handy function
            DefaultMessageIDTypes packetIdentifier = (DefaultMessageIDTypes)GetPacketIdentifier(p);

            // Check if this is a network message packet
            switch (packetIdentifier)
            {
                case DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION:
                    // Connection lost normally
                    Debug.Log("ID_DISCONNECTION_NOTIFICATION\n");
                    break;
                case DefaultMessageIDTypes.ID_ALREADY_CONNECTED:
                    // Connection lost normally
                    Debug.Log("ID_ALREADY_CONNECTED with guid " + p.guid);
                    break;
                case DefaultMessageIDTypes.ID_INCOMPATIBLE_PROTOCOL_VERSION:
                    Debug.Log("ID_INCOMPATIBLE_PROTOCOL_VERSION\n");
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_DISCONNECTION_NOTIFICATION: // Server telling the clients of another client disconnecting gracefully.  You can manually broadcast this in a peer to peer enviroment if you want.
                    Debug.Log("ID_REMOTE_DISCONNECTION_NOTIFICATION\n");
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_CONNECTION_LOST: // Server telling the clients of another client disconnecting forcefully.  You can manually broadcast this in a peer to peer enviroment if you want.
                    Debug.Log("ID_REMOTE_CONNECTION_LOST\n");
                    break;
                case DefaultMessageIDTypes.ID_REMOTE_NEW_INCOMING_CONNECTION: // Server telling the clients of another client connecting.  You can manually broadcast this in a peer to peer enviroment if you want.
                    Debug.Log("ID_REMOTE_NEW_INCOMING_CONNECTION\n");
                    break;
                case DefaultMessageIDTypes.ID_CONNECTION_BANNED: // Banned from this server
                    Debug.Log("We are banned from this server.\n");
                    break;
                case DefaultMessageIDTypes.ID_CONNECTION_ATTEMPT_FAILED:
                    Debug.Log("Connection attempt failed\n");
                    break;
                case DefaultMessageIDTypes.ID_NO_FREE_INCOMING_CONNECTIONS:
                    // Sorry, the server is full.  I don't do anything here but
                    // A real app should tell the user
                    Debug.Log("ID_NO_FREE_INCOMING_CONNECTIONS\n");
                    break;

                case DefaultMessageIDTypes.ID_INVALID_PASSWORD:
                    Debug.Log("ID_INVALID_PASSWORD\n");
                    break;

                case DefaultMessageIDTypes.ID_CONNECTION_LOST:
                    // Couldn't deliver a reliable packet - i.e. the other system was abnormally
                    // terminated
                    Debug.Log("ID_CONNECTION_LOST\n");
                    break;

                case DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED:
                    // This tells the client they have connected
                    Debug.Log("ID_CONNECTION_REQUEST_ACCEPTED to "+ p.systemAddress.ToString(true) + " with GUID "+p.guid.ToString());
                    Debug.Log("My external address is "+ client.GetExternalID(p.systemAddress).ToString(true));
                    break;
                case DefaultMessageIDTypes.ID_CONNECTED_PING:
                case DefaultMessageIDTypes.ID_UNCONNECTED_PING:
                    Debug.Log("Ping from %s\n"+p.systemAddress.ToString(true));
                    break;
                default:
                    // It's a client, so just show the message
                    Debug.Log(System.Text.Encoding.UTF8.GetString(p.data));
                    break;
            }
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
}
