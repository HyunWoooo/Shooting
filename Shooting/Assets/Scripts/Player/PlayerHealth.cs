using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int startinghealth = 100;
    public int cuurenthealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f; 
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;                                              
    AudioSource playerAudio;                                    
    PlayerMovement playerMovement;                              
    PlayerShooting playerShooting;
    PlayerBuff playerBuff;
    YouDie playerDie;
    GameObject damageText;

    GameObject HitParticle;

    Clear clear;

    bool isDead;                                                
    bool damaged;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               

    // Use this for initialization
    void Awake() {

        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        playerBuff = GetComponent<PlayerBuff>();
        playerDie = GameObject.Find("You_Die").GetComponent<YouDie>();
        damageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;
        clear = GameObject.Find("Clear").GetComponent<Clear>();

        cuurenthealth = startinghealth;
    }
	
	// Update is called once per frame
	void Update () {

        if(damaged)
        {
            damageImage.color = flashColour;
        }

        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;


    }

    public void TakeUserDamage(int damage, Vector3 hitpoint)
    {
        if (isDead || clear.GetClear())
            return;
        damaged = true;

        if (playerBuff.DFS_BUFF())
        {
            damage = damage / 2;
        }

        cuurenthealth -= damage;

        healthSlider.value = cuurenthealth;

        damageText = Instantiate(damageText, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;

        damageText.transform.parent = this.transform.FindChild("PlayerCanvas");

        damageText.GetComponent<Text>().text = "-" + damage;

        damageText.transform.localScale = new Vector3(1, 1, 1);

        damageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;

        playerAudio.Play();

        HitParticle = Resources.Load("Prefabs/HitParticles") as GameObject;

        HitParticle = Instantiate(HitParticle, hitpoint, Quaternion.Euler(0, 0, 0)) as GameObject;

        if (cuurenthealth <= 0 && isDead == false)
        {
            Death();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || clear.GetClear())
            return;
        damaged = true;

        if(playerBuff.DFS_BUFF())
        {
            damage = damage / 2;
        }

        cuurenthealth -= damage;

        healthSlider.value = cuurenthealth;

        damageText = Instantiate(damageText, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;

        damageText.transform.parent = this.transform.FindChild("PlayerCanvas");

        damageText.GetComponent<Text>().text = "-" + damage;

        damageText.transform.localScale = new Vector3(1,1,1);

        damageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;

        playerAudio.Play();

        if(cuurenthealth <= 0 && isDead == false)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerDie.Die();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public bool isDeadF()
    {
        return isDead;
    }
}
