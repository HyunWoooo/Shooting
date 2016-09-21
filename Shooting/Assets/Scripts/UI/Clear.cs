using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Network;

public class Clear : MonoBehaviour {

    bool clear;
    float alpha;
    BattleRoomNet net;
    TestClient clinet;
    WwwMain www;

    Image background;
    Text text;

    ScoreManager scoreManager;

    float timer;

    // Use this for initialization
    void Start()
    {
        
        background = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        if (Application.loadedLevelName == "BattleRoom")
        {
            net = GameObject.Find("BattleRoomNet").GetComponent<BattleRoomNet>();
            clinet = GameObject.Find("Network").GetComponent<TestClient>();
            www = GameObject.Find("WWW").GetComponent<WwwMain>();
            text.text = "Win";
        }

    }

    // Update is called once per frame
    void Update () {
        float offset = 0.5f;
        if (clear)
        {
            //net.stop = true;
            timer += Time.deltaTime;
            background.color = new Vector4(255, 255, 255, alpha + offset * Time.deltaTime);
            text.color = new Vector4(255, 255, 0, alpha + offset * Time.deltaTime);
            alpha = background.color.a;

            if (timer > 3f)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (Application.loadedLevelName == "SingleMode")
                        Application.LoadLevel("ReStart");
                    else if (Application.loadedLevelName == "BattleRoom")
                    {
                        www.SetWin();
                        www.SetScore(2);
                        www.ScoreUpdate();
                        clinet.CloseClient();
                        Application.LoadLevel("Lobby");
                    }
                }
            }
        }

    }

    public void ClearF()
    {
        clear = true;
    }

    public bool GetClear()
    {
        return clear;
    }
}
