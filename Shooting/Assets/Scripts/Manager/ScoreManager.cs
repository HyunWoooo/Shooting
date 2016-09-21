using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    int score = 0;
    bool clear;
    Clear cClear;

    bool ScoreManagerInit;

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "SingleMode")
            cClear = GameObject.Find("Clear").GetComponent<Clear>();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Plus_Score(int plus)
    {
        score += plus;
    }

    public int Score()
    {
        return score;
    }

    public void Clear()
    {
        clear = true;
        cClear.ClearF();
    }
}
