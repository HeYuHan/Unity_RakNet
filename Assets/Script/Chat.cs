using UnityEngine;
using System.Collections;
using RakNet;
using System.Net.Sockets;
using System;

public class Chat : MonoBehaviour {

    UdpConnection connection;
    NetworkStream stream;
    // Use this for initialization
    void Start () {
        connection = new UdpConnection();
        stream = new NetworkStream();
        connection.m_Stream = stream;
        stream.connection = connection;
        connection.m_OnConnected = OnConnected;
        connection.m_OnDisconnected = OnDisconnected;
        connection.Connect("127.0.0.1", 9500, "channel");
    }
    public void OnMessage()
    {
        
        Debug.Log("OnMessage");

    }
    public void OnDisconnected()
    {
        Debug.Log("OnDisconnected");
    }
    public void OnConnected()
    {
        Debug.Log("OnConnected");
        stream.BeginWrite();
        stream.WriteByte(1);
        stream.WriteInt(2);
        stream.WriteFloat(1.1f);
        stream.EndWrite();
    }
    private void OnApplicationQuit()
    {
        connection.DisConnect();
    }
    // Update is called once per frame
    void Update () {
        connection.Update();
    }

}
