using UnityEngine;
using System.Collections;

public class Logout : MonoBehaviour {

    WwwMain www;

    // Use this for initialization
    void Start () {

        www = GameObject.Find("WWW").GetComponent<WwwMain>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LogoutButton()
    {
        www.LogOutButton();
        Application.LoadLevel("Login");
    }
}
