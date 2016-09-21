using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class EnemyTrace : EnemyState
{
    static EnemyTrace enemyTrace;

    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.TRACE;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        enemyAi.Trace();
        if (!enemyAi.bSight())
            enemyAi.ChangeState(EnemyPatrol.Instance());
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static EnemyTrace Instance()
    {
        //if (enemyTrace == null)
            enemyTrace = ScriptableObject.CreateInstance("EnemyTrace") as EnemyTrace;
        return enemyTrace;
    }
}
