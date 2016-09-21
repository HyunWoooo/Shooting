using UnityEngine;
using System.Collections;

public class HellephantRespawn : MonoBehaviour {

    GameObject hellephant;
    ScoreManager scoreManager;
    bool create;

    float timer;

    // Use this for initialization
    void Start () {

        hellephant = Resources.Load("Prefabs/Mover/Hellephant") as GameObject;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }
	
	// Update is called once per frame
	void Update () {

        if (scoreManager.Score() >= 40 && !create)
        {
            hellephant = Instantiate(hellephant, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            create = true;
        }

    }
}
