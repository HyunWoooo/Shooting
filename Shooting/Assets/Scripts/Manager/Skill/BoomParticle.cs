using UnityEngine;
using System.Collections;

public class BoomParticle : MonoBehaviour {

    EnemyPlayerInfo enemyBoom = null;
    bool damage;

	// Use this for initialization
	void Start () {

        if(Application.loadedLevelName == "BattleRoom")
        {
            enemyBoom = GameObject.Find("EnemyPlayer").GetComponent<EnemyPlayerInfo>();
        }

        Destroy(this.gameObject, 1f);

    }
	
	// Update is called once per frame
	void Update () {


    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && !damage)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamage(50,new Vector3(0,0,0));
            damage = true;
        }

        if (Application.loadedLevelName == "BattleRoom")
        {
            if ((enemyBoom.temp_skill[2] || enemyBoom.temp_skill[1]) && other.gameObject.tag == "Player" && !damage)
            {
                PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();

                enemyHealth.TakeDamage(50);
                damage = true;
            }
        }
    }
}
