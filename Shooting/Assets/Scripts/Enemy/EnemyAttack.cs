using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public int attackdamage = 10;
    public float attckcooltime = 1f;

    Animator anim;
    GameObject Player;
    PlayerHealth playerhealth;

    bool playerInRange;
    float timer;
    float range;

    // Use this for initialization
    void Awake () {

        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerhealth = Player.GetComponent<PlayerHealth>();

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (playerInRange == true && timer >= attckcooltime)
        {
            Attack();
        }
            
	
        if(playerhealth.cuurenthealth <= 0)
            anim.SetTrigger("PlayerDead");
	}

    void Attack()
    {
        if(playerhealth.cuurenthealth > 0)
            playerhealth.TakeDamage(attackdamage);

        timer = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        range = (transform.position - Player.transform.position).magnitude;

        if (other.gameObject == Player && range <= GetComponent<SphereCollider>().radius * 2.2f)
        {
            playerInRange = true;
        }
           
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player && range > 0.8)
            playerInRange = false;
    }
}
