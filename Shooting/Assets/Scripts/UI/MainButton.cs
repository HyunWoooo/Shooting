using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainButton : MonoBehaviour {

    BGM bgm;

    // Use this for initialization
    void Start () {

        bgm = GameObject.Find("Sound").GetComponent<BGM>();
        
        if (SceneManager.GetActiveScene().name == "StartMenu")
            bgm.BGMPlay();
        else if (!bgm.audio.isPlaying)
            bgm.BGMPlay();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SingleButton()
    {
        SceneManager.LoadScene("SingleMode");
    }

    public void MultiButton()
    {
        SceneManager.LoadScene("Login");
    }

    public void CreatorButton()
    {
        SceneManager.LoadScene("Creator");
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QutiButton()
    {
        Application.Quit();
    }
}
