using UnityEngine;
using System.Collections;
using nEnemyState;
using nEnemyAI;

public class BossEarthQuake : EnemyState
{
    static BossEarthQuake bossEarthQuake;


    override public void Enter(EnemyAI enemyAi)
    {
        state = (int)State.STAND;
    }

    override public void Execute(EnemyAI enemyAi)
    {
        HellephantAI helle = enemyAi as HellephantAI;
        
        helle.Skill_EarthQuake();

        switch (helle.Active())
        {
            case 0:
                break;
            case 1:
                helle.ChangeState(BossRush.Instance());
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

    public static BossEarthQuake Instance()
    {
        if (bossEarthQuake == null)
            bossEarthQuake = ScriptableObject.CreateInstance("BossEarthQuake") as BossEarthQuake;

        return bossEarthQuake;
    }
}
