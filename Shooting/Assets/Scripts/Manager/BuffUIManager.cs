using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuffUIManager : MonoBehaviour {

    Text ATK_text;
    Text DPS_text;
    Text SPD_text;

    PlayerBuff playerBuff;

    bool BuffUIManagerInit;

    // Use this for initialization
    void Start () {
        ATK_text = GameObject.Find("ATK").GetComponent<Text>();
        DPS_text = GameObject.Find("DPS").GetComponent<Text>();
        SPD_text = GameObject.Find("SPD").GetComponent<Text>();

        playerBuff = GameObject.Find("Player").GetComponent<PlayerBuff>();

    }
	
	// Update is called once per frame
	void Update () {

        if (playerBuff.ATK_BUFF())
        {
            ATK_text.enabled = true;
        }
        else
        {
            ATK_text.enabled = false;
        }

        if (playerBuff.DFS_BUFF())
        {
            DPS_text.enabled = true;
        }
        else
        {
            DPS_text.enabled = false;
        }

        if (playerBuff.SPD_BUFF())
        {
            SPD_text.enabled = true;
        }
        else
        {
            SPD_text.enabled = false;
        }

    }
}
