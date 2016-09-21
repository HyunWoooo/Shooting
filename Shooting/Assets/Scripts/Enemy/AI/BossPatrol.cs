using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class BossPatrol : EnemyPatrol
{
    static BossPatrol bossPatrol;


    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.PATROL;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        enemyAi.Patrol();

        if (enemyAi.bSight())
        {
            enemyAi.ChangeState(BossTrace.Instance());
        }

    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static new EnemyPatrol Instance()
    {
        if (bossPatrol == null)
            bossPatrol = ScriptableObject.CreateInstance("BossPatrol") as BossPatrol;
        return bossPatrol;
    }
}
