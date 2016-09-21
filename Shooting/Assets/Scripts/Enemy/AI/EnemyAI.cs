using UnityEngine;
using System.Collections;
using nEnemyState;

namespace nEnemyAI
{

    public class EnemyAI : MonoBehaviour
    {
        protected Transform player;
        protected NavMeshAgent nav;
        protected EnemyHealth health;

        EnemyAI enemyAi;
        protected EnemyState currentState;

        protected float viewAngle = 120;
        protected bool sight;
        protected bool patrol;

        protected Vector3 patrolpoint;

        protected float timer;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            nav = GetComponent<NavMeshAgent>();
            enemyAi = GetComponent<EnemyAI>();
            health = GetComponent<EnemyHealth>();

            currentState = EnemyPatrol.Instance();
        }

        // Update is called once per frame
        void Update()
        { 
            if (currentState)
            {
                currentState.Execute(enemyAi);
            }
        }

        public void ChangeState(EnemyState NewState)
        {
            currentState.Exit(enemyAi);

            currentState = NewState;

            currentState.Enter(enemyAi);

        }

        public void Stand()
        {
            
        }

        public void Patrol()
        {
            if (health.crruenthealth <= 0)
                return;

            timer += Time.deltaTime;

            if (timer > 4f)
            {
                patrol = false;
                timer = 0;
            }
            if (patrol == false)
            {
                patrolpoint = new Vector3(Random.Range(-10f, 10f) + this.transform.position.x, 0f, Random.Range(-10f, 10f) + this.transform.position.z);

                nav.SetDestination(patrolpoint);

                patrol = true;
            }

            float range;

            range = (transform.position - patrolpoint).magnitude;

            if (range < 1.0f)
            {
                patrol = false;
            }

            if (health.GetDamaged())
            {
                transform.LookAt(player.position);
                health.SetDamaged(false);
            }
        }

        public void Trace()
        {
            if (health.crruenthealth <= 0)
                return;

            if (health.GetDamaged())
            {
                transform.LookAt(player.position);
                health.SetDamaged(false);
            }

            patrol = false;
            nav.enabled = true;
            nav.SetDestination(player.position);
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle <= viewAngle * 0.5f)
                {
                    sight = true;
                }
                else
                {
                    sight = false;
                }
            }
        }

        public bool bSight()
        {
            return sight;
        }
    }

}