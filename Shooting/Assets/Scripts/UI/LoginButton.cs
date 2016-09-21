using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginButton : MonoBehaviour {

    Button button;

    // Use this for initialization
    void Start()
    {

        button = GetComponent<Button>();

        button.onClick.AddListener(() => GameObject.Find("WWW").GetComponent<WwwMain>().LoginButton());
    }
}
