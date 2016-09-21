using UnityEngine;
using System.Collections;

public class AimMove : MonoBehaviour {

    Transform player;
    Transform gun;
    PlayerShooting shooting;
    RectTransform rect;

    Vector3 aimPos;

    // Use this for initialization
    void Awake() {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        gun = player.FindChild("Gun").transform;
        shooting = gun.GetComponentInChildren<PlayerShooting>();
        rect = GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {

        aimPos = Camera.main.WorldToScreenPoint(shooting.gunAimPoint) + new Vector3(-25,-25,0); ;

        rect.anchoredPosition = aimPos;
    }
}
