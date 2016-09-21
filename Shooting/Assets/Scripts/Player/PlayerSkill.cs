using UnityEngine;
using System.Collections;


public class PlayerSkill : MonoBehaviour
{
    GameObject MultiShoot;
    GameObject BombThrow;
    GameObject CannonAim;
    GameObject CannonBarrage;
    Shader RimLight;
    Shader MotionBlur;
    Shader Standard;
    Renderer rend;

    bool Multi;
    bool Boom;
    bool Cannon;
    bool Accel;

    bool Accelshader;

    public float MultiShootCool;
    float Multi_timer;
    public float BombThrowCool;
    float Boom_timer;
    public float CannonBarrageCool;
    float Cannon_timer;
    public float AccelCool;
    float Accel_timer;

    // Use this for initialization
    void Start()
    {
        MultiShoot = Resources.Load("Prefabs/Skill/MultiShoot") as GameObject;
        MultiShootCool = 10;

        BombThrow = Resources.Load("Prefabs/Skill/BombThrow") as GameObject;
        BombThrowCool = 10;

        CannonAim = Resources.Load("Prefabs/Skill/CannonAim") as GameObject;
        CannonBarrageCool = 60;

        RimLight = Resources.Load("Shader/RimLight") as Shader;
        Standard = Shader.Find("Standard (Specular setup)");
        MotionBlur = Shader.Find("Custom/SurMotionBlur");
        //MotionBlur = Shader.Find("Unlit/MotionBlur");

        rend = GetComponentInChildren<Renderer>();

        AccelCool = 10;
    }

    // Update is called once per frame
    void Update()
    {
        MultiShootF();
        BombThrowF();
        CannonBarrageF();
        AccelF();
    }

    void MultiShootF()
    {
        if (Input.GetKey(KeyCode.Alpha1) && Multi == false)
        {
            MultiShoot = Instantiate(MultiShoot, this.transform.position, this.transform.rotation) as GameObject;
            MultiShoot.transform.parent = GameObject.Find("Gun").transform;
            MultiShoot = Resources.Load("Prefabs/Skill/MultiShoot") as GameObject;
            Multi = true;
        }

        if (Multi == true)
        {
            Multi_timer += Time.deltaTime;
            if (Multi_timer >= 0.1f)
            {
                MultiShootCool -= 0.1f;
                Multi_timer = 0;
            }
        }

        if (MultiShootCool <= 0)
        {
            MultiShootCool = 10;
            Multi = false;
        }
    }

    void BombThrowF()
    {
        if (Input.GetKey(KeyCode.Alpha2) && Boom == false)
        {
            BombThrow = Instantiate(BombThrow, this.transform.position + (Vector3.forward * 1.5f), this.transform.rotation) as GameObject;
            BombThrow = Resources.Load("Prefabs/Skill/BombThrow") as GameObject;
            Boom = true;
        }

        if (Boom == true)
        {
            Boom_timer += Time.deltaTime;
            if (Boom_timer >= 0.1f)
            {
                BombThrowCool -= 0.1f;
                Boom_timer = 0;
            }
        }

        if (BombThrowCool <= 0)
        {
            BombThrowCool = 10;
            Boom = false;
        }
    }

    void CannonBarrageF()
    {
        if (Input.GetKey(KeyCode.Alpha3) && Cannon == false)
        {
            CannonAim = Resources.Load("Prefabs/Skill/CannonAim") as GameObject;
            CannonAim = Instantiate(CannonAim, this.transform.position + new Vector3(0,0.1f,0), Quaternion.Euler(90,0,0)) as GameObject;
            
            Cannon = true;
        }

        if (Cannon == true)
        {
            Cannon_timer += Time.deltaTime;
            if (Cannon_timer >= 0.1f)
            {
                CannonBarrageCool -= 0.1f;
                Cannon_timer = 0;
            }
        }

        if (CannonBarrageCool <= 0)
        {
            CannonBarrageCool = 60;
            Cannon = false;
        }
    }

    void AccelF()
    {
        if (Input.GetKey(KeyCode.Alpha4) && Accel == false)
        {
            //rend.material.shader = MotionBlur;
            //rend.material.shader = RimLight;
            //rend.material.SetVector("_RimColor", new Vector4(1,1,0,1));
            Accel = true;
            Accelshader = true;
        }

        if (Accel == true)
        {
            Accel_timer += Time.deltaTime;
            if (Accel_timer >= 0.1f)
            {
                AccelCool -= 0.1f;
                Accel_timer = 0;
            }
        }

        if(AccelCool <= 9.2f && AccelCool > 0)
        {
            Accelshader = false;
            //if (rend.material.shader != Standard)
            //{
            //    //rend.material.shader = Standard;
                
            //}
        }

        else if (AccelCool <= 0)
        {
            AccelCool = 10;
            Accel = false;
        }
    }

    public bool MultiActive()
    {
        return Multi;
    }

    public bool BoomActive()
    {
        return Boom;
    }

    public bool CannonActive()
    {
        return Cannon;
    }

    public bool AccelActive()
    {
        return Accel;
    }

    public bool AccelS()
    {
        return Accelshader;
    }

    public GameObject GetCannonAim()
    {
        return CannonAim;
    }
}