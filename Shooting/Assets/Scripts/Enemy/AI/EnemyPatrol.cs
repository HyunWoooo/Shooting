using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;


public class EnemyPatrol : EnemyState {

    static EnemyPatrol enemyPatrol;


    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.PATROL;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        enemyAi.Patrol();
        if (enemyAi.bSight())
        {
            enemyAi.ChangeState(EnemyTrace.Instance());
        }
            
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static EnemyPatrol Instance()
    {
        //if (enemyPatrol == null)
            enemyPatrol = ScriptableObject.CreateInstance("EnemyPatrol") as EnemyPatrol;
        return enemyPatrol;
    }
}
