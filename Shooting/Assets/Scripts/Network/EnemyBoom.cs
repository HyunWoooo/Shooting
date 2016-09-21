using UnityEngine;
using System.Collections;

public class EnemyBoom : MonoBehaviour {

    Transform Player;
    GameObject BoomParticle;

    float timer;

    // Use this for initialization
    void Start()
    {

        Player = GameObject.Find("EnemyPlayer").transform;
        transform.GetComponent<Rigidbody>().AddForce((Player.transform.forward + Player.transform.up) * 300);

        Destroy(this.gameObject, 2.1f);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 2)
        {
            BoomParticle = Resources.Load("Prefabs/Skill/BoomParticle") as GameObject;
            BoomParticle = Instantiate(BoomParticle, this.transform.position, this.transform.rotation) as GameObject;
            timer = 0;
        }

    }
}
