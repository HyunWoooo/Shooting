using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUIManager : MonoBehaviour {

    PlayerSkill skill;

    float MultiShootCool;
    float BombThrowCool;
    float CannonBarrageCool;
    float AccelCool;

    Text Multitext;
    Text Bombtext;
    Text Cannontext;
    Text Acceltext;

    Image MultiImage;
    Image BombImage;
    Image CannonImage;
    Image AccelImage;

    bool SkillUIManagerInit;

    // Use this for initialization
    void Start () {
        Multitext = GameObject.Find("MultiShoot").GetComponentInChildren<Text>();
        Bombtext = GameObject.Find("BombThrow").GetComponentInChildren<Text>();
        Cannontext = GameObject.Find("CannonBarrage").GetComponentInChildren<Text>();
        Acceltext = GameObject.Find("Accel").GetComponentInChildren<Text>();

        MultiImage = GameObject.Find("MultiShoot").GetComponent<Image>();
        BombImage = GameObject.Find("BombThrow").GetComponent<Image>();
        CannonImage = GameObject.Find("CannonBarrage").GetComponent<Image>();
        AccelImage = GameObject.Find("Accel").GetComponent<Image>();

        skill = GameObject.Find("Player").GetComponent<PlayerSkill>();

    }


	
	// Update is called once per frame
	void Update () {

        //if (Application.loadedLevelName == "BattleRoom" || Application.loadedLevelName == "SingleMode" && !SkillUIManagerInit)
        //{
        //    Multitext = GameObject.Find("MultiShoot").GetComponentInChildren<Text>();
        //    Bombtext = GameObject.Find("BombThrow").GetComponentInChildren<Text>();
        //    Cannontext = GameObject.Find("CannonBarrage").GetComponentInChildren<Text>();
        //    Acceltext = GameObject.Find("Accel").GetComponentInChildren<Text>();

        //    MultiImage = GameObject.Find("MultiShoot").GetComponent<Image>();
        //    BombImage = GameObject.Find("BombThrow").GetComponent<Image>();
        //    CannonImage = GameObject.Find("CannonBarrage").GetComponent<Image>();
        //    AccelImage = GameObject.Find("Accel").GetComponent<Image>();

        //    skill = GameObject.Find("Player").GetComponent<PlayerSkill>();
        //    SkillUIManagerInit = true;
        //}

        //if (Application.loadedLevelName != "BattleRoom" || Application.loadedLevelName != "SingleMode")
        //{
        //    SkillUIManagerInit = false;
        //    return;
        //}

        MultiShooCoolTime();
        BombThrowCoolTime();
        CannonBarrgeCoolTime();
        AccelCoolTime();
          
    }

    void MultiShooCoolTime()
    {
        if(skill.MultiShootCool == 10)
        {
            Multitext.text = "Multi\nShoot";
            MultiImage.fillAmount = 1;
        }
        else
        {
            Multitext.text = "cooltime: " + (int)skill.MultiShootCool;
            MultiShootCool = (float)skill.MultiShootCool / 10;
            MultiImage.fillAmount = 1 - MultiShootCool;
        }
    }

    void BombThrowCoolTime()
    {
        if (skill.BombThrowCool == 10)
        {
            Bombtext.text = "Bomb\nThrow";
            BombImage.fillAmount = 1;
        }
        else
        {
            Bombtext.text = "cooltime: " + (int)skill.BombThrowCool;
            BombThrowCool = (float)skill.BombThrowCool / 10;
            BombImage.fillAmount = 1 - BombThrowCool;
        }
    }

    void CannonBarrgeCoolTime()
    {
        if (skill.CannonBarrageCool == 60)
        {
            Cannontext.text = "Cannon\nBarrge";
            CannonImage.fillAmount = 1;
        }
        else
        {
            Cannontext.text = "cooltime: " + (int)skill.CannonBarrageCool;
            CannonBarrageCool = (float)skill.CannonBarrageCool / 60;
            CannonImage.fillAmount = 1 - CannonBarrageCool;
        }
    }

    void AccelCoolTime()
    {
        if (skill.AccelCool == 10)
        {
            Acceltext.text = "Accel";
            AccelImage.fillAmount = 1;
        }
        else
        {
            Acceltext.text = "cooltime: " + (int)skill.AccelCool;
            AccelCool = (float)skill.AccelCool / 10;
            AccelImage.fillAmount = 1 - AccelCool;
        }
    }
}
