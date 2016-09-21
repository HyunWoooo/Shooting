using UnityEngine;
using System.Collections;

public class MultiShoot : MonoBehaviour {

    // Use this for initialization
    void Start () {

        if(transform.parent.parent.name == "Player")
        {
            EnemyShooting[] eshoot = transform.GetComponentsInChildren<EnemyShooting>();
            eshoot[0].enabled = false;
            eshoot[1].enabled = false;
            eshoot[2].enabled = false;
        }

        if (transform.parent.parent.name == "EnemyPlayer")
        {
            PlayerShooting[] pshoot = transform.GetComponentsInChildren<PlayerShooting>();
            pshoot[0].enabled = false;
            pshoot[1].enabled = false;
            pshoot[2].enabled = false;
        }

        Destroy(this.gameObject, 5f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
