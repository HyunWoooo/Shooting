using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Network;

public class YouDie : MonoBehaviour {

    bool die;
    float alpha;

    BattleRoomNet net;
    TestClient clinet;
    WwwMain www;

    Image background;
    Text text;

    float timer;

    // Use this for initialization
    void Start () {

        background = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();

        if (Application.loadedLevelName == "BattleRoom")
        {
            net = GameObject.Find("BattleRoomNet").GetComponent<BattleRoomNet>();
            clinet = GameObject.Find("Network").GetComponent<TestClient>();
            www = GameObject.Find("WWW").GetComponent<WwwMain>();
        }

    }
	
	// Update is called once per frame
	void Update () {

        float offset = 0.5f;
        
        if (die)
        {
            //if (timer > 2f)
            //    net.stop = true;
            timer += Time.deltaTime;
            background.color = new Vector4(0, 0, 0, alpha + offset * Time.deltaTime);
            text.color = new Vector4(255, 0, 0, alpha + offset * Time.deltaTime);
            alpha = background.color.a;

            if (timer > 3f)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (Application.loadedLevelName == "SingleMode")
                        Application.LoadLevel("ReStart");
                    else if (Application.loadedLevelName == "BattleRoom")
                    {
                        www.SetLose();
                        www.SetScore(-1);
                        www.ScoreUpdate();
                        clinet.CloseClient();
                        Application.LoadLevel("Lobby");
                    }
                }
            }
        }
	
	}

    public void Die()
    {
        die = true;
    }
}
