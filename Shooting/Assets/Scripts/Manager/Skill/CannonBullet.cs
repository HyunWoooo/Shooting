using UnityEngine;
using System.Collections;

public class CannonBullet : MonoBehaviour {

    GameObject BoomParticle;

    bool trigger;

    float timer;

    // Use this for initialization
    void Start () {

        BoomParticle = Resources.Load("Prefabs/Skill/BoomParticle") as GameObject;

    }
	
	// Update is called once per frame
	void Update () {

        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0f,-1f,0f) * 30);

        //timer += Time.deltaTime;
        //
        //if (timer >= 2)
        //{
        //    BoomParticle = Instantiate(BoomParticle, this.transform.position, this.transform.rotation) as GameObject;
        //    timer = 0;
        //}

    }

    void OnTriggerEnter(Collider other)
    {
        if(trigger == false && other.name == "Floor")
        {
            BoomParticle = Instantiate(BoomParticle, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(this.gameObject, 1f);
            trigger = true;
        }
    }
}
