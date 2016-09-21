using UnityEngine;
using System.Collections;

public class R_Particle : MonoBehaviour {

    EnemyMovement Hellephant;

    // Use this for initialization
    void Start () {

        
        Destroy(this.gameObject,1f);

    }
	
	// Update is called once per frame
	void Update () {

        Hellephant = GameObject.Find("Hellephant(Clone)").GetComponent<EnemyMovement>();
        Hellephant.Rush();

    }
}
