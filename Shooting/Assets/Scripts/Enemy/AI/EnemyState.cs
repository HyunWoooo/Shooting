using UnityEngine;
using System.Collections;
using nEnemyAI;

namespace nEnemyState
{

    public class EnemyState : ScriptableObject
    {
        protected enum State
        {
            STAND = 1,
            TRACE,
            PATROL
        };

        protected int state;

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        virtual public void Enter(EnemyAI enemyAi)
        {
            
        }

        virtual public void Execute(EnemyAI enemyAi)
        {

        }

        virtual public void Exit(EnemyAI enemyAi)
        {

        }

        public int GetState()
        {
            return state;
        }
    }

}