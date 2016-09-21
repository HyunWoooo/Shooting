using UnityEngine;
using System.Collections;
using Network;

public class PInfo : MonoBehaviour {

    struct preInfo
    {
        public Vector3 pos;
        public Quaternion rot;
        public int hp;
        public bool shoot;
        public bool walking;
        public bool death;
        public bool[] skill;
        public Vector3 cpos;
    };

    public PlayerInfo player_Info;
    PlayerHealth player_Health;
    PlayerShooting player_Shoot;
    PlayerMovement player_Move;
    PlayerSkill player_skill;
    preInfo pre_Info;

    public bool send;

    // Use this for initialization
    void Start () {

        player_Info = new PlayerInfo((int)PROTOCOL.PLAYER_INFO);
        player_Health = GetComponent<PlayerHealth>();
        player_Shoot = GetComponentInChildren<PlayerShooting>();
        player_Move = GetComponent<PlayerMovement>();
        player_skill = GetComponent<PlayerSkill>();

    }
	
	// Update is called once per frame
	void Update () {

        pre_Info.skill = new bool[4];

        player_Info.playerPos = transform.position;
        player_Info.playerRot = transform.rotation;
        player_Info.health = player_Health.cuurenthealth;
        player_Info.shoot = player_Shoot.shoot;
        player_Info.walking = player_Move.GetWalking();
        player_Info.death = player_Health.isDeadF();
        player_Info.skill[0] = player_skill.MultiActive();
        player_Info.skill[1] = player_skill.BoomActive();
        player_Info.skill[3] = player_skill.AccelS();
        if (GameObject.FindGameObjectsWithTag("SkillAim").Length > 0)
        {
            player_Info.skill[2] = player_skill.GetCannonAim().GetComponent<CannonBarrage>().Active();
            player_Info.CannonPos = player_skill.GetCannonAim().transform.position;
        }
        else
            player_Info.skill[2] = false;

        if (Vector3.SqrMagnitude(player_Info.playerPos - pre_Info.pos) > 0.01f)
        {
            pre_Info.pos = player_Info.playerPos;
            send = true;
        }
        if(pre_Info.rot != player_Info.playerRot)
        {
            pre_Info.rot = player_Info.playerRot;
            send = true;
        }
        if(pre_Info.hp != player_Info.health)
        {
            pre_Info.hp = player_Info.health;
            send = true;
        }
        if (pre_Info.shoot != player_Info.shoot)
        {
            pre_Info.shoot = player_Info.shoot;
            send = true;
        }
        if (pre_Info.walking != player_Info.walking)
        {
            pre_Info.walking = player_Info.walking;
            send = true;
        }
        if (pre_Info.death != player_Info.death)
        {
            pre_Info.death = player_Info.death;
            send = true;
        }
        if (pre_Info.skill[0] != player_Info.skill[0])
        {
            pre_Info.skill[0] = player_Info.skill[0];
            send = true;
        }
        if (pre_Info.skill[1] != player_Info.skill[1])
        {
            pre_Info.skill[1] = player_Info.skill[1];
            send = true;
        }
        if (pre_Info.skill[2] != player_Info.skill[2])
        {
            pre_Info.skill[2] = player_Info.skill[2];
            send = true;
        }
        if (pre_Info.skill[3] != player_Info.skill[3])
        {
            pre_Info.skill[3] = player_Info.skill[3];
            send = true;
        }
        if (pre_Info.cpos != player_Info.CannonPos)
        {
            pre_Info.cpos = player_Info.CannonPos;
            send = true;
        }
    }
}
