using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    BGM bgm;
    public AudioClip bgmclip;

    // Use this for initialization
    void Start () {

        bgm = GameObject.Find("Sound").GetComponent<BGM>();
        if (!bgm.audio.isPlaying)
            bgm.BGMPlay();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RoomButton()
    {
        Application.LoadLevel("MultiMode");
    }
}
