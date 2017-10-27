using UnityEngine;
using System;
using RakNet;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        try
        {
            RakString dllCallTest = new RakString();
        }
        catch (Exception e)
        {
            Debug.Log("DLL issue\nAdd SwigOutput/CplusDLLIncludes/RakNetWrap.cxx to the project\nDLL_Swig/RakNet.sln and rebuild.\nPress enter to quit.");
            return;
        }

        Packet testPacket;
        int loopNumber;
        RakNet.BitStream stringTestSendBitStream = new RakNet.BitStream();
        RakNet.BitStream rakStringTestSendBitStream = new RakNet.BitStream();
        RakNet.BitStream receiveBitStream = new RakNet.BitStream();
        String holdingString;
        TimeSpan startTimeSpan;
        RakString rakStringTest = new RakString();

        RakPeerInterface testClient = RakPeer.GetInstance();

        testClient.Startup(1, new SocketDescriptor(60000, "127.0.0.1"), 1);

        RakPeerInterface testServer = RakPeer.GetInstance();
        testServer.Startup(1, new SocketDescriptor(60001, "127.0.0.1"), 1);
        testServer.SetMaximumIncomingConnections(1);

        Console.WriteLine("Send and receive loop using BitStream.\nBitStream read done into RakString");

        testClient.Connect("127.0.0.1", 60001, "", 0);

        String sendString = "The test string";
        stringTestSendBitStream.Write((byte)DefaultMessageIDTypes.ID_USER_PACKET_ENUM);
        stringTestSendBitStream.Write(sendString);

        RakString testRakString = new RakString("Test RakString");
        rakStringTestSendBitStream.Write((byte)DefaultMessageIDTypes.ID_USER_PACKET_ENUM);
        rakStringTestSendBitStream.Write(testRakString);

        startTimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        loopNumber = 0;

        while (startTimeSpan.TotalSeconds + 5 > (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds)
        {
            testPacket = testServer.Receive();
            if (testPacket != null && testPacket.data[0] == (byte)DefaultMessageIDTypes.ID_USER_PACKET_ENUM)
            {
                receiveBitStream.Reset();
                receiveBitStream.Write(testPacket.data, testPacket.length);
                receiveBitStream.IgnoreBytes(1);
                receiveBitStream.Read(rakStringTest);
                Debug.Log("Loop number: " + loopNumber + "\nData: " + rakStringTest.C_String());
            }
            testServer.DeallocatePacket(testPacket);
            loopNumber++;
            System.Threading.Thread.Sleep(50);
            testClient.Send(rakStringTestSendBitStream, PacketPriority.LOW_PRIORITY, PacketReliability.RELIABLE_ORDERED, (char)0, new AddressOrGUID(new SystemAddress("127.0.0.1", 60001)), false);
        }

        Debug.Log("String send and receive loop using BitStream.\nBitStream read done into String");

        SystemAddress[] remoteSystems;
        ushort numberOfSystems = 1;
        testServer.GetConnectionList(out remoteSystems, ref numberOfSystems);

        startTimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
        loopNumber = 0;
        while (startTimeSpan.TotalSeconds + 5 > (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds)
        {
            testPacket = testServer.Receive();
            if (testPacket != null && testPacket.data[0] == (byte)DefaultMessageIDTypes.ID_USER_PACKET_ENUM)
            {
                receiveBitStream.Reset();
                receiveBitStream.Write(testPacket.data, testPacket.length);
                receiveBitStream.IgnoreBytes(1);
                receiveBitStream.Read(out holdingString);
                Debug.Log("Loop number: " + loopNumber + "\nData: " + holdingString);
            }
            testServer.DeallocatePacket(testPacket);
            loopNumber++;
            System.Threading.Thread.Sleep(50);
            SystemAddress sa = RakNet.RakNet.UNASSIGNED_SYSTEM_ADDRESS;
            testClient.Send(stringTestSendBitStream, PacketPriority.LOW_PRIORITY, PacketReliability.RELIABLE_ORDERED, (char)0, new AddressOrGUID(new SystemAddress("127.0.0.1", 60001)), false);
        }
        //If RakString is not freed before program exit it will crash
        rakStringTest.Dispose();
        testRakString.Dispose();

        RakPeer.DestroyInstance(testClient);
        RakPeer.DestroyInstance(testServer);
        Debug.Log("Demo complete. Press Enter.");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
