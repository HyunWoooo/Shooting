using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class EnemyStand : EnemyState
{
    static EnemyStand enemyStand;


    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.STAND;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        enemyAi.Stand();
        if (enemyAi.bSight())
            enemyAi.ChangeState(EnemyTrace.Instance());
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(enemyStand);
        Destroy(this);
    }

    public static EnemyStand Instance()
    {
        enemyStand = ScriptableObject.CreateInstance("EnemyStand") as EnemyStand;
        
        return enemyStand;
    }
}
