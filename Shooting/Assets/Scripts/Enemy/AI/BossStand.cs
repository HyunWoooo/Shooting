using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class BossStand : EnemyStand
{
    static BossStand bossStand;


    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.STAND;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        enemyAi.Stand();
        if (enemyAi.bSight())
            enemyAi.ChangeState(BossTrace.Instance());
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static new BossStand Instance()
    {
        if (bossStand == null)
            bossStand = ScriptableObject.CreateInstance("BossStand") as BossStand;

        return bossStand;
    }
}
