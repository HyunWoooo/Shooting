using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Network;

public class GameMain : MonoBehaviour {

    string input_text;
    List<string> received_texts;

    TestClient client;
    Packet packet;

    CMessage message;
    ReadyInfo readyInfo;

    SocketAsyncEventArgs e;

    Vector2 currentScrollPos;

    // Use this for initialization
    void Start () {
        message = new CMessage((int)PROTOCOL.MESSAGE);
        packet = new Packet();
        readyInfo = new ReadyInfo((int)PROTOCOL.GAME_READY);
        currentScrollPos = new Vector2();
        client =GameObject.Find("Network").GetComponent<TestClient>();
        this.input_text = "";
        this.received_texts = new List<string>();
        e = new SocketAsyncEventArgs();

    }

    void Update()
    {
    }

    public void on_receive_chat_msg(string text)
    {
        this.received_texts.Add(text);
        this.currentScrollPos.y = float.PositiveInfinity;
    }

    void OnGUI()
    {
        // Received text.
        GUILayout.BeginVertical();
        currentScrollPos = GUILayout.BeginScrollView(currentScrollPos,
            GUILayout.MaxWidth(Screen.width), GUILayout.MinWidth(Screen.width),
            GUILayout.MaxHeight(Screen.height - 100), GUILayout.MinHeight(Screen.height - 100));

        foreach (string text in this.received_texts)
        {
            GUILayout.BeginHorizontal();
            GUI.skin.label.wordWrap = true;
            GUILayout.Label(text);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();


        // Input.
        GUILayout.BeginHorizontal();
        this.input_text = GUILayout.TextField(this.input_text, GUILayout.MaxWidth(Screen.width - 250), GUILayout.MinWidth(Screen.width - 250),
            GUILayout.MaxHeight(50), GUILayout.MinHeight(50));

        if (GUILayout.Button("Send", GUILayout.MaxWidth(100), GUILayout.MinWidth(100), GUILayout.MaxHeight(50), GUILayout.MinHeight(50)))
        {
            //packet.msg_create(input_text);
            //e.SetBuffer(packet.GetPacket(), 0, packet.GetPacket().Length);

            message.SetMessage(input_text);
            //messageInfo.SetMessage(input_text);

            byte[] buffer = new byte[512];
            //Packet.Serialize(messageInfo).CopyTo(buffer, 0);
            
            Packet.Serialize(message).CopyTo(buffer, 0);
            packet.Packet_Create(buffer);
            e.SetBuffer(packet.GetPacket(), 0, packet.GetPacket().Length);

            client.send(e);

            this.input_text = "";
        }
        if(GUILayout.Button("Ready", GUILayout.MaxWidth(100), GUILayout.MinWidth(100), GUILayout.MaxHeight(50), GUILayout.MinHeight(50)))
        {
            string text = "Player Ready";
            readyInfo.SetReady();

            byte[] buffer = new byte[512];

            Packet.Serialize(readyInfo).CopyTo(buffer, 0);
            packet.Packet_Create(buffer);
            e.SetBuffer(packet.GetPacket(), 0, packet.GetPacket().Length);

            client.send(e);

            on_receive_chat_msg(text);
        }
        GUILayout.EndHorizontal();
    }
}
