using UnityEngine;
using System.Collections;
using nEnemyAI;

public class HellephantAI : BossAI
{
    enum Skill
    {
        EARTHQUAKE = 0,
        RUSH,
    }

    float[] skillTimer;
    bool[] skillActive;

    HellephantAI hellephantAi;

    GameObject eq_Range;
    GameObject eq_Particle;
    bool earthquake;
    float alpha;

    GameObject r_Range;
    GameObject r_Particle;
    bool rush;

    float range;
    int active;
    bool activing;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();    
        health = GetComponent<EnemyHealth>();

        currentState = BossStand.Instance();

        hellephantAi = GetComponent<HellephantAI>();
        eq_Range = Resources.Load("Prefabs/Skill/BossSkill/EQ_Range") as GameObject;
        eq_Particle = Resources.Load("Prefabs/Skill/BossSkill/EQ_Particle") as GameObject;
        r_Range = Resources.Load("Prefabs/Skill/BossSkill/Rush_Range") as GameObject;
        r_Particle = Resources.Load("Prefabs/Skill/BossSkill/Rush_Particle") as GameObject;

        skillTimer = new float[4];
        skillActive = new bool[4] { true, true , true , true };
        active = 2;

    }
	
	// Update is called once per frame
	void Update () {

        PatternSelect();

        if (currentState)
        {
            currentState.Execute(hellephantAi);
        }

    }

    public void PatternSelect()
    {
        timer += Time.deltaTime;

        range = (transform.position - player.transform.position).magnitude;

        for(int i=0; i< skillTimer.Length; i++)
        {
            if (!skillActive[i])
            {
                skillTimer[i] += Time.deltaTime;
                if (skillTimer[i] >= 10)
                {
                    skillActive[i] = true;
                    skillTimer[i] = 0;
                }
            }
        }

        if (timer > 3)
        {
            while (true)
            {
                if (activing)
                    break;

                int random = (int)Random.Range(0, 4); 

                switch (random)
                {
                    case 0:
                        if (range <= 10 && skillActive[(int)Skill.EARTHQUAKE])
                        {
                            active = 0;
                            return;
                        }
                        break;
                    case 1:
                        if (range <= 30 && skillActive[(int)Skill.RUSH])
                        {
                            active = 1;
                            return;
                        }
                        break;
                    case 2:
                        if (skillActive[2] && !skillActive[(int)Skill.EARTHQUAKE] && !skillActive[(int)Skill.RUSH])
                        {
                            active = 2;
                            return;
                        }
                        
                        break;
                    case 3:
                        
                        return;
                }
            }
            timer = 0;
        }
    }

    public void Skill_EarthQuake()
    {
        if (skillActive[(int)Skill.EARTHQUAKE] == false)
            return;

        activing = true;

        if (earthquake == false)
        {
            eq_Range = Instantiate(eq_Range, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
            eq_Range.transform.parent = transform;
            earthquake = true;
        }

        if (eq_Range != null)
        {
            float offset = 0.5f;
            eq_Range.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, alpha + offset * Time.deltaTime);
            alpha = eq_Range.GetComponent<SpriteRenderer>().color.a;
            

            if (eq_Range.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                eq_Particle = Instantiate(eq_Particle, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                Destroy(eq_Range);

                eq_Range = Resources.Load("Prefabs/Skill/BossSkill/EQ_Range") as GameObject;
                eq_Particle = Resources.Load("Prefabs/Skill/BossSkill/EQ_Particle") as GameObject;
                alpha = 0;
                earthquake = false;
                skillActive[(int)Skill.EARTHQUAKE] = false;
                activing = false;
            }
        }
    }

    public void Skill_Rush()
    {
        if (skillActive[(int)Skill.RUSH] == false)
            return;

        activing = true;

        if (rush == false)
        {
            r_Range = Instantiate(r_Range, transform.position + transform.forward * 12f, Quaternion.Euler(90, transform.eulerAngles.y, 0)) as GameObject;
            r_Range.transform.parent = transform;
            rush = true;
        }

        if (r_Range != null)
        {
            float offset = 0.5f;
            r_Range.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, alpha + offset * Time.deltaTime);
            alpha = r_Range.GetComponent<SpriteRenderer>().color.a;

            if (r_Range.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                r_Particle = Instantiate(r_Particle, transform.position - transform.forward * 3f, Quaternion.Euler(-90, 0, 0)) as GameObject;
                r_Particle.transform.parent = transform;
                Destroy(r_Range);

                r_Range = Resources.Load("Prefabs/Skill/BossSkill/Rush_Range") as GameObject;
                r_Particle = Resources.Load("Prefabs/Skill/BossSkill/Rush_Particle") as GameObject;
                alpha = 0;
                rush = false;
                skillActive[(int)Skill.RUSH] = false;
                activing = false;
            }
        }
    }

    public int Active()
    {
        return active;
    }
}
