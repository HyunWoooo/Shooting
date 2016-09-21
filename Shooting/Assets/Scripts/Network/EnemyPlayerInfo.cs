using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyPlayerInfo : MonoBehaviour {

    enum SKILL
    {
        MULTI,
        BOOM,
        CANNON,
        ACCEL,
    };

    public Slider healthSlider;
    public AudioClip deathClip;
    GameObject HitParticle;
    AudioSource playerAudio; 
    Animator anim;
    Renderer rend;
    Shader RimLight;
    Shader Standard;

    GameObject MultiShoot;
    GameObject BombThrow;
    GameObject CannonAim;
    GameObject CannonBarrage;

    public bool shoot;
    public int health;
    public bool walking;
    public bool death;
    public bool[] skill;
    public bool[] temp_skill;
    public Vector3 skill_AimPos;

    Clear clear;

    int num;
    // Use this for initialization
    void Start () {

        clear = GameObject.Find("Clear").GetComponent<Clear>();
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<Renderer>();
        RimLight = Resources.Load("Shader/RimLight") as Shader;
        Standard = Shader.Find("Standard (Specular setup)");
        health = 1000;
        num = 0;

        skill = new bool[4];
        temp_skill = new bool[4];
    }
	
	// Update is called once per frame
	void Update () {

        healthSlider.value = health;

        if (death && num == 0)
        {
            num = 1;
            clear.ClearF();
            Death();
        }

        if (!death)
        {

            anim.SetBool("IsWalking", walking);

            if (skill[(int)SKILL.MULTI] && !temp_skill[(int)SKILL.MULTI])
            {
                temp_skill[(int)SKILL.MULTI] = true;
                MultiShoot = Resources.Load("Prefabs/Skill/MultiShoot") as GameObject;
                MultiShoot = Instantiate(MultiShoot, this.transform.position, this.transform.rotation) as GameObject;
                MultiShoot.transform.parent = GameObject.Find("EGun").transform;
            }
            else if (!skill[(int)SKILL.MULTI])
                temp_skill[(int)SKILL.MULTI] = false;

            if (skill[(int)SKILL.BOOM] && !temp_skill[(int)SKILL.BOOM])
            {
                temp_skill[(int)SKILL.BOOM] = true;
                BombThrow = Resources.Load("Prefabs/Skill/EnemyBombThrow") as GameObject;
                BombThrow = Instantiate(BombThrow, this.transform.position + (Vector3.forward * 1.5f), this.transform.rotation) as GameObject;
            }
            else if (!skill[(int)SKILL.BOOM])
                temp_skill[(int)SKILL.BOOM] = false;

            if (skill[(int)SKILL.CANNON] && !temp_skill[(int)SKILL.CANNON])
            {
                temp_skill[(int)SKILL.CANNON] = true;
                CannonAim = Resources.Load("Prefabs/Skill/CannonAim") as GameObject;
                CannonAim = Instantiate(CannonAim, skill_AimPos + new Vector3(0, 0.1f, 0), Quaternion.Euler(90, 0, 0)) as GameObject;
                CannonAim.GetComponent<CannonBarrage>().only_rot = true;
            }
            else if (!skill[(int)SKILL.CANNON])
                temp_skill[(int)SKILL.CANNON] = false;

            if (skill[(int)SKILL.ACCEL] && !temp_skill[(int)SKILL.ACCEL])
            {
                temp_skill[(int)SKILL.ACCEL] = true;
                rend.material.shader = RimLight;
                rend.material.SetVector("_RimColor", new Vector4(1, 1, 0, 1));
            }
            else if (!skill[(int)SKILL.ACCEL])
            {
                temp_skill[(int)SKILL.ACCEL] = false;
                rend.material.shader = Standard;
            }
        }
    }

    void Death()
    {
        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetRot(Quaternion rot)
    {
        transform.rotation = rot;
    }

    public void SetShoot(bool sht)
    {
        Debug.Log(shoot);
        shoot = sht;
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }

    public void SetWalking(bool walk)
    {
        walking = walk;
    }

    public void SetDeath(bool isdead)
    {
        death = isdead;
    }

    public void SetSkill(bool[] skillactive)
    {
        skill = skillactive;
    }

    public void SetAimPos(Vector3 pos)
    {
        skill_AimPos = pos;
    }

    public void Hit(Vector3 hitpoint)
    {
        HitParticle = Resources.Load("Prefabs/HitParticles") as GameObject;
        HitParticle = Instantiate(HitParticle, hitpoint, Quaternion.Euler(0, 0, 0)) as GameObject;
    }
}
