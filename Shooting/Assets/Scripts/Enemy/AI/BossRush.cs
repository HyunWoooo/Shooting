using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class BossRush : EnemyState
{
    static BossRush bossRush;

    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.STAND;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        HellephantAI helle = enemyAi as HellephantAI;

        helle.Skill_Rush();

        switch (helle.Active())
        {
            case 0:
                helle.ChangeState(BossEarthQuake.Instance());
                break;
            case 1:
                
                break;
            case 2:
                helle.ChangeState(BossTrace.Instance());
                break;
            default:
                break;

        }
    }

    override public void Exit(EnemyAI enemyAi)
    {
        Destroy(this);
    }

    public static BossRush Instance()
    {
        if (bossRush == null)
            bossRush = ScriptableObject.CreateInstance("BossRush") as BossRush;

        return bossRush;
    }
}
