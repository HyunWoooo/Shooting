using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {

    BGM bgm;

    // Use this for initialization
    void Start () {

        bgm = GameObject.Find("Sound").GetComponent<BGM>();
        bgm.BGMStop();
        //DontDestroyOnLoad(this.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
