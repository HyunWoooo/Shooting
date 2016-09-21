using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Network
{
    enum PROTOCOL
    {
        MESSAGE = 0,

        GAME_READY,

        GAME_START,

        ROOM_IN,

        All_ROOM_IN,

        PLAYER_INFO,
    };

    public class TestClient : MonoBehaviour
    {

        TcpClient client;

        GameMain game_main;

        string received_texts;

        SocketAsyncEventArgs recv_E;
        SocketAsyncEventArgs send_E;

        CMessage message;
        ReadyInfo readyInfo;

        Packet packet;
        byte[] pbuffer;

        PInfo pInfo;
        PlayerInfo playerInfo;

        EnemyPlayerInfo enemyInfo;

        int GameStart;

        byte[] id_temp;
        byte[] temp;

        bool room_in;

        // Use this for initialization
        void Start()
        {
            IPHostEntry host;
            string myIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily.ToString() == "InterNetwork")
                {
                    myIP = ip.ToString();
                }
            }
            client = new TcpClient();
            client.Connect("203.130.66.30", 9000);
            //client.Connect("221.147.174.147", 9000);

            game_main = GameObject.Find("Chatting").GetComponent<GameMain>();

            recv_E = new SocketAsyncEventArgs();
            recv_E.Completed += new System.EventHandler<SocketAsyncEventArgs>(recv);
            recv_E.SetBuffer(new byte[512], 0, 512);

            message = new CMessage();
            readyInfo = new ReadyInfo((int)PROTOCOL.ROOM_IN);
            playerInfo = new PlayerInfo();

            packet = new Packet();
            send_E = new SocketAsyncEventArgs();
            pbuffer = new byte[512];

            client.Client.ReceiveAsync(recv_E);

            id_temp = new byte[4];
            temp = new byte[4];

            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if(GameStart == 1 && Application.loadedLevelName != "BattleRoom")
            {
                Buffer.BlockCopy(recv_E.Buffer, 4, id_temp, 0, 4);
                Application.LoadLevel("BattleRoom");
            }
            if(GameStart == 1 && Application.loadedLevelName == "BattleRoom")
            {
                readyInfo.SetReady();
                pInfo = GameObject.Find("Player").GetComponent<PInfo>();
                enemyInfo = GameObject.Find("EnemyPlayer").GetComponent<EnemyPlayerInfo>();

                pInfo.player_Info.user_id = BitConverter.ToInt32(id_temp, 0);

                enemyInfo.transform.position = new Vector3(-10, 0, 0);
                pInfo.transform.position = new Vector3(0, 0, 0);

                if (pInfo.player_Info.user_id == 2)
                {
                    pInfo.transform.position = new Vector3(-10, 0, 0);
                    enemyInfo.transform.position = new Vector3(0, 0, 0);
                }

                Packet.Serialize(readyInfo).CopyTo(pbuffer, 0);
                packet.Packet_Create(pbuffer);
                send_E.SetBuffer(packet.GetPacket(), 0, packet.GetPacket().Length);

                send(send_E);

                GameStart++;
            }
            if(GameStart == 2 && Application.loadedLevelName == "BattleRoom")
            {
                if (playerInfo.user_id == pInfo.player_Info.user_id)
                    return;

                enemyInfo.SetPos(playerInfo.playerPos);
                enemyInfo.SetRot(playerInfo.playerRot);
                enemyInfo.SetHealth(playerInfo.health);
                enemyInfo.SetShoot(playerInfo.shoot);
                enemyInfo.SetWalking(playerInfo.walking);
                enemyInfo.SetDeath(playerInfo.death);
                enemyInfo.SetSkill(playerInfo.skill);
                enemyInfo.SetAimPos(playerInfo.CannonPos);
            }
        }

        public void send(SocketAsyncEventArgs e)
        {
            try
            {
                client.Client.SendAsync(e);
            }
            catch
            {
                Debug.Log("SendAsync Error");
            }
        }

        public void recv(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                int sum = 0;

                Buffer.BlockCopy(recv_E.Buffer, 0, temp, 0, 4);
                sum = BitConverter.ToInt32(temp, 0);

                switch (sum)
                {
                    case (int)PROTOCOL.MESSAGE:
                        //string data = System.Text.Encoding.UTF8.GetString(recv_E.Buffer);
                        message = (CMessage)Packet.Deserialize(recv_E.Buffer, message.GetType());

                        game_main.on_receive_chat_msg(message.GetMessageA());
                        client.Client.ReceiveAsync(recv_E);
                        break;
                    case (int)PROTOCOL.GAME_READY:

                        client.Client.ReceiveAsync(recv_E);
                        break;
                    case (int)PROTOCOL.GAME_START:
                        GameStart = 1;

                        client.Client.ReceiveAsync(recv_E);
                        break;
                    case (int)PROTOCOL.ROOM_IN:

                        client.Client.ReceiveAsync(recv_E);
                        break;

                    case (int)PROTOCOL.All_ROOM_IN:
                        room_in = true;

                        client.Client.ReceiveAsync(recv_E);
                        break;
                    case (int)PROTOCOL.PLAYER_INFO:

                        playerInfo = (PlayerInfo)Packet.Deserialize(recv_E.Buffer, playerInfo.GetType());

                        client.Client.ReceiveAsync(recv_E);
                        break;
                }
            }
            catch
            {
                Debug.Log("recv Error");
            }
        }

        public void CloseClient()
        {
            client.Client.Close();
            Destroy(this.gameObject);
        }

        public bool RoomIN()
        {
            return room_in;
        }
    }
}