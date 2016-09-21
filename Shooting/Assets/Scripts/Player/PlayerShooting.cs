using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public int shootdamage = 10;
    public float shootcooltime = 0.15f;
    public float range = 100f;
    public Vector3 gunAimPoint;

    float timer;
    int shootablemask;
    float effectsDisplayTime = 0.25f;
    Ray gunRay;
    RaycastHit gunRayHit;
    AudioSource gunAudio;
    ParticleSystem gunParticle;
    LineRenderer gunLine;
    Light gunLight;
    PlayerBuff playerBuff;

    EnemyPlayerInfo enemy_Player;

    public bool shoot;


    // Use this for initialization
    void Start () {

        shootablemask = LayerMask.GetMask("Shootable");

        gunAudio = GetComponent<AudioSource>();
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        playerBuff = GameObject.Find("Player").GetComponent<PlayerBuff>();

        if (Application.loadedLevelName == "BattleRoom")
            enemy_Player = GameObject.Find("EnemyPlayer").GetComponent<EnemyPlayerInfo>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        Aimming();

        if(timer >= shootcooltime && Input.GetButton("Fire1"))
        {
            Shoot();
            shoot = true;
        }
        else if(!Input.GetButton("Fire1"))
            shoot = false;

        if (timer >= shootcooltime * effectsDisplayTime)
        {
            DisableEffects();
        }
	
	}

    void Shoot()
    {
        timer = 0;

        gunAudio.Play();

        gunParticle.Stop();
        gunParticle.Play();

        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        gunRay.origin = transform.position;
        gunRay.direction = transform.forward + new Vector3(1f, 1f, 0f) * Random.Range(-0.03f, 0.03f) + new Vector3(-1f, 1f, 0f) * Random.Range(-0.03f, 0.03f);

        if (Physics.Raycast(gunRay, out gunRayHit, range, shootablemask))
        {
            EnemyHealth enemyHealth = null;

            if (gunRayHit.collider.tag == "Enemy")
            {
                enemyHealth = gunRayHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    if (playerBuff.ATK_BUFF())
                        shootdamage = 12;
                    else
                        shootdamage = 10;

                    enemyHealth.TakeDamage(shootdamage, gunRayHit.point);
                }
            }

            else if (gunRayHit.collider.tag == "EnemyPlayer")
            {
                enemy_Player.Hit(gunRayHit.point);
            }

            gunLine.SetPosition(1, gunRayHit.point);
        }
        else
        {
            gunLine.SetPosition(1, gunRay.origin + gunRay.direction * range);
        }
    }

    void Aimming()
    {
        gunRay.origin = transform.position;
        gunRay.direction = transform.forward;

        if (Physics.Raycast(gunRay, out gunRayHit, range, shootablemask))
        {
            gunAimPoint = gunRayHit.point;
        } 
    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
