using UnityEngine;
using System.Collections;

public class EQ_Particle : MonoBehaviour {

    GameObject player;
    PlayerHealth health;
    bool takedamage;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();

        Destroy(this.gameObject, 0.5f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.transform.position.y <= 0.1f && !takedamage)
        {
            health.TakeDamage(20);
            takedamage = true;
        }
            
    }
}
