using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    Transform Player;
    PlayerSkill playerSkill;
    PlayerMovement playerMovement;
    CannonBarrage cannonBarrage;

    Image aimImage;

    bool Cannon;
    int camera_state = 0;

    int count = 0;

    bool InputManagerInit;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player").transform;
        playerSkill = Player.GetComponent<PlayerSkill>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        aimImage = GameObject.Find("aim").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

        if (!playerSkill.CannonActive())
        {
            Cannon = false;
            count = 0;
        }

        if (playerSkill.CannonActive() && Cannon == false)
        {
            camera_state = 1; // 캐논카메라

            cannonBarrage = GameObject.Find("CannonAim(Clone)").GetComponent<CannonBarrage>();

            playerMovement.enabled = false;

            aimImage.enabled = false;

            Cannon = true;
        }

        else if(Cannon && count == 0)
        {
            if(cannonBarrage.Active())
            {
                camera_state = 0; // 디폴트 카메라

                playerMovement.enabled = true;

                aimImage.enabled = true;

                count++;
            }
        }
	}

    public int Camera_State()
    {
        return camera_state;
    }
}
