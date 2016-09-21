using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int starthealth;
    public int crruenthealth;
    public AudioClip deathClip;
    public Slider healthSlider;

    Animator anim;
    AudioSource enemyAudio;
    GameObject damageText;
    GameObject HitParticle;

    ScoreManager scoreManager;

    float sinkingspeed = 2f;
    bool sinking;
    bool isDead;
    bool damaged;


    // Use this for initialization
    void Awake() {

        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        damageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;
        HitParticle = Resources.Load("Prefabs/HitParticles") as GameObject;

        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        crruenthealth = starthealth;

    }
	
	// Update is called once per frame
	void Update () {

        if (sinking)
            transform.Translate(-Vector3.up * sinkingspeed * Time.deltaTime);

    }

    public void TakeDamage(int damage, Vector3 hitpoint)
    {
        if (isDead)
            return;

        damaged = true;

        crruenthealth -= damage;

        healthSlider.value = crruenthealth;

        damageText = Instantiate(damageText, new Vector3(0,0,0), Quaternion.Euler(0,180,0)) as GameObject;

        damageText.transform.parent = this.transform.FindChild("EnemyCanvas");

        damageText.GetComponent<Text>().text = "-"+damage;

        damageText.transform.localScale = new Vector3(10, 10, 1);

        damageText = Resources.Load("Prefabs/UI/DamageText") as GameObject;

        HitParticle = Instantiate(HitParticle, hitpoint, Quaternion.Euler(0, 0, 0)) as GameObject;

        HitParticle = Resources.Load("Prefabs/HitParticles") as GameObject;

        enemyAudio.Play();

        if(crruenthealth <= 0 && isDead == false)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        anim.SetTrigger("Dead");

        if(transform.name == "Hellephant(Clone)")
        {
            scoreManager.Plus_Score(100);
            scoreManager.Clear();
        }
        else
            scoreManager.Plus_Score(10);
    }

    public void StartSinking()
    {
        sinking = true;

        GetComponent<NavMeshAgent>().enabled = false; // 플레이어를 추적 안함
        GetComponent<Rigidbody>().isKinematic = true; // 충돌검사 안함

        Destroy(gameObject, 2f);
    }

    public void SetDamaged(bool set)
    {
        damaged = set;
    }

    public bool GetDamaged()
    {
        return damaged;
    }
}
