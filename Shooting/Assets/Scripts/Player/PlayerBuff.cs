using UnityEngine;
using System.Collections;
using BuffItem;

public class PlayerBuff : MonoBehaviour {

    GameObject ATK_text;
    GameObject DFS_text;
    GameObject SPD_text;
    Shader RimLight;
    Shader MotionBlur;
    Shader Standard;
    Renderer rend;
    PlayerSkill playerSkill;

    bool ATK_buff;
    bool DFS_buff;
    bool SPD_buff;

    float ATK_timer;
    float DFS_timer;
    float SPD_timer;

    // Use this for initialization
    void Start () {

        ATK_text = Resources.Load("Prefabs/UI/ATKText") as GameObject;
        DFS_text = Resources.Load("Prefabs/UI/DFSText") as GameObject;
        SPD_text = Resources.Load("Prefabs/UI/SPDText") as GameObject;
        RimLight = Resources.Load("Shader/RimLight") as Shader;
        Standard = Shader.Find("Standard (Specular setup)");
        //Standard = Shader.Find("Unlit/MotionBlur");
        MotionBlur = Shader.Find("Unlit/MotionBlur");
        playerSkill = GetComponent<PlayerSkill>();

        rend = GetComponentInChildren<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        if (ATK_buff)
            AtackBuffTimer();

        if (DFS_buff)
            DefenseBuffTimer();

        if (SPD_buff)
            SpeedBuffTimer();

        if (!ATK_buff && !DFS_buff && !SPD_buff && !playerSkill.AccelActive())
            rend.material.shader = Standard;
    }

    void AtackBuffTimer()
    {
        ATK_timer += Time.deltaTime;

        if (ATK_timer >= 10)
        {
            ATK_buff = false;

            ATK_timer = 0;
        }
    }

    void DefenseBuffTimer()
    {
        DFS_timer += Time.deltaTime;

        if (DFS_timer >= 10)
        {
            DFS_buff = false;

            DFS_timer = 0;
        }
    }

    void SpeedBuffTimer()
    {
        SPD_timer += Time.deltaTime;

        if (SPD_timer >= 10)
        {
            SPD_buff = false;

            SPD_timer = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            switch(other.GetComponent<Buff>().GetBuff())
            {
                case 0:
                    ATK_buff = true;

                    ATK_text = Instantiate(ATK_text, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
                    ATK_text.transform.parent = this.transform.FindChild("PlayerCanvas");
                    ATK_text = Resources.Load("Prefabs/UI/ATKText") as GameObject;

                    rend.material.shader = RimLight;
                    rend.material.SetVector("_RimColor", new Vector4(1, 0, 0, 1));
                    break;

                case 1:
                    DFS_buff = true;

                    DFS_text = Instantiate(DFS_text, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
                    DFS_text.transform.parent = this.transform.FindChild("PlayerCanvas");
                    DFS_text = Resources.Load("Prefabs/UI/DFSText") as GameObject;

                    rend.material.shader = RimLight;
                    rend.material.SetVector("_RimColor", new Vector4(0, 0, 1, 1));
                    break;

                case 2:
                    SPD_buff = true;

                    SPD_text = Instantiate(SPD_text, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
                    SPD_text.transform.parent = this.transform.FindChild("PlayerCanvas");
                    SPD_text = Resources.Load("Prefabs/UI/SPDText") as GameObject;

                    rend.material.shader = RimLight;
                    rend.material.SetVector("_RimColor", new Vector4(1, 1, 0, 1));
                    break;
            }
        }
    }

    public bool ATK_BUFF()
    {
        return ATK_buff;
    }

    public bool DFS_BUFF()
    {
        return DFS_buff;
    }

    public bool SPD_BUFF()
    {
        return SPD_buff;
    }
}
