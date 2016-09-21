using UnityEngine;
using System.Collections;

public class ZombunnyRespawn : MonoBehaviour {

    GameObject zombunny;

    float timer;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer > 5)
        {
            zombunny = Resources.Load("Prefabs/Mover/Zombunny") as GameObject;
            zombunny = Instantiate(zombunny, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            timer = 0;
        }
	
	}
}
