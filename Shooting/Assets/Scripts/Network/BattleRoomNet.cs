using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Network;

public class BattleRoomNet : MonoBehaviour {

    TestClient client;
    Packet packet;

    PInfo pInfo;

    SocketAsyncEventArgs e;

    CMessage message;
    PlayerInfo playerInfo;

    float timer;
    byte[] pbuffer;

    public bool stop;

    // Use this for initialization
    void Start () {

        client = GameObject.Find("Network").GetComponent<TestClient>();
        packet = new Packet();

        pInfo = GameObject.Find("Player").GetComponent<PInfo>();

        message = new CMessage((int)PROTOCOL.PLAYER_INFO);
        playerInfo = new PlayerInfo((int)PROTOCOL.PLAYER_INFO);

        e = new SocketAsyncEventArgs();
        pbuffer = new byte[512];
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (pInfo.send && client.RoomIN())
        {
            Packet.Serialize(pInfo.player_Info).CopyTo(pbuffer, 0);
            packet.Packet_Create(pbuffer);
            e.SetBuffer(packet.GetPacket(), 0, packet.GetPacket().Length);

            client.send(e);

            pInfo.send = false;

        }

    }
}
