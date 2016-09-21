using UnityEngine;
using System.Collections;

public class BackMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackButton()
    {
        Application.LoadLevel("ReStart");
        //Application.LoadLevel("Lobby");
    }
}
