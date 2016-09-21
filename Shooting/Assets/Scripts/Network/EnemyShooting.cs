using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    int shootdamage = 10;
    float shootcooltime = 0.15f;
    float effectsDisplayTime = 0.2f;

    EnemyPlayerInfo enemy_Info;

    Ray gunRay;
    RaycastHit gunRayHit;
    AudioSource gunAudio;
    ParticleSystem gunParticle;
    LineRenderer gunLine;
    Light gunLight;
    int shootablemask;

    float timer;

    // Use this for initialization
    void Start () {

        enemy_Info = GameObject.Find("EnemyPlayer").GetComponent<EnemyPlayerInfo>();

        shootablemask = LayerMask.GetMask("Shootable");

        gunAudio = GetComponent<AudioSource>();
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= shootcooltime && enemy_Info.shoot)
        {
            Shoot();
        }

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

        float range = 100;

        if (Physics.Raycast(gunRay, out gunRayHit, range, shootablemask))
        {
            PlayerHealth enemyHealth = null;

            if (gunRayHit.collider.tag == "Player")
            {
                enemyHealth = gunRayHit.collider.GetComponent<PlayerHealth>();

                if (enemyHealth != null && enemyHealth.cuurenthealth > 0)
                {
                    shootdamage = 10;

                    enemyHealth.TakeUserDamage(shootdamage, gunRayHit.point);
                }
            }

            gunLine.SetPosition(1, gunRayHit.point);
        }
        else
        {
            gunLine.SetPosition(1, gunRay.origin + gunRay.direction * range);
        }
    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
