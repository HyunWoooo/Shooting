using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonA()
    {
        text.text = "왼쪽으로 갑니다.";
    }

    public void ButtonS()
    {
        text.text = "뒤쪽으로 갑니다.";
    }

    public void ButtonD()
    {
        text.text = "오른쪽으로 갑니다.";
    }

    public void ButtonW()
    {
        text.text = "위쪽으로 갑니다.";
    }

    public void Button1()
    {
        text.text = "1번 스킬 입니다.";
    }

    public void Button2()
    {
        text.text = "2번 스킬 입니다.";
    }

    public void Button3()
    {
        text.text = "3번 스킬 입니다.";
    }

    public void Button4()
    {
        text.text = "4번 스킬 입니다.";
    }

    public void ButtonSpace()
    {
        text.text = "점프 합니다.";
    }

    public void MouseL()
    {
        text.text = "총을 쏩니다";
    }
}
