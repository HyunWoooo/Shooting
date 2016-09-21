using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateUser : MonoBehaviour {

    Button button;

    // Use this for initialization
    void Start () {

        button = GetComponent<Button>();

        button.onClick.AddListener(() => GameObject.Find("WWW").GetComponent<WwwMain>().CreateUser());

    }

}
