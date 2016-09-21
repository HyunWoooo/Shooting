using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    //Transform player;
    //NavMeshAgent nav;

    //EnemyHealth health;

    //float viewAngle = 120;
    //float targetMask;
    //bool sight;
    //bool patrol;

    //Vector3 patrolpoint;

    //float timer;

    // Use this for initialization
    void Start () {

        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //nav = GetComponent<NavMeshAgent>();

        //health = GetComponent<EnemyHealth>();

        //targetMask = LayerMask.GetMask("EnemyTarget");
    }

    // Update is called once per frame
    void Update() {

        //if (sight == true && health.crruenthealth > 0)
        //{
        //    Trace();
        //    patrol = false;
        //}   
        //else if (health.crruenthealth > 0)
        //    Patrol();

        //if(health.GetDamaged())
        //{
        //    transform.LookAt(player.position);
        //    health.SetDamaged(false);
        //}

    }

    //void Trace()
    //{
    //    nav.enabled = true;
    //    nav.SetDestination(player.position);
    //}

    //void Patrol()
    //{
    //    timer += Time.deltaTime;

    //    if(timer > 4f)
    //    {
    //        patrol = false;
    //        timer = 0;
    //    }
    //    if (patrol == false)
    //    {
    //        patrolpoint = new Vector3(Random.Range(-10f, 10f) + this.transform.position.x, 0f, Random.Range(-10f, 10f) + this.transform.position.z);

    //        nav.SetDestination(patrolpoint);

    //        patrol = true;
    //    }

    //    float range;

    //    range = (transform.position - patrolpoint).magnitude;

    //    if (range < 1.0f)
    //    {
    //        patrol = false;
    //    }
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        Vector3 direction = other.transform.position - transform.position;
    //        float angle = Vector3.Angle(direction, transform.forward);

    //        if (angle <= viewAngle * 0.5f)
    //        {
    //            sight = true;
    //        }
    //        else
    //        {
    //            sight = false;
    //        }
    //    }
    //}

    public void Rush()
    {
        transform.Translate(Vector3.forward * 30f * Time.deltaTime);
    }
}
