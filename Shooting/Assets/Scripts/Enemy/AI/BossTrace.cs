using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class BossTrace : EnemyTrace
{
    static BossTrace bossTrace;

    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.TRACE;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        HellephantAI helle = enemyAi as HellephantAI;

        helle.Trace();
        switch(helle.Active())
        {
            case 0:
                helle.ChangeState(BossEarthQuake.Instance());
                break;
            case 1:
                helle.ChangeState(BossRush.Instance());
                break;
            case 2:
                
                break;
            default:
                break;

        }
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static new EnemyTrace Instance()
    {
        if (bossTrace == null)
            bossTrace = ScriptableObject.CreateInstance("BossTrace") as BossTrace;
        return bossTrace;
    }
}
