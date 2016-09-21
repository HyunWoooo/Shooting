using UnityEngine;
using System.Collections;

public class CannonBarrage : MonoBehaviour {

    GameObject CannonBullet;

    int floorMask;
    Ray camRay;
    RaycastHit floorHit;
    Vector3 bulletPos;

    bool active = false;
    bool state_camera = true;

    float timer;

    public bool only_rot;

    // Use this for initialization
    void Start () {

        CannonBullet = Resources.Load("Prefabs/Skill/CannonBullet") as GameObject;

        floorMask = LayerMask.GetMask("Floor");

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0,0,0.5f);

        if (only_rot)
        {
            Destroy(this.gameObject, 5f);
            Attack();
            return;
        }
            

        if (active == false)
            Move();
        else
            Attack();

    }

    void Move()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Destroy(this.gameObject, 5f);
            active = true;
            state_camera = false;
            return;
        }

        if (Physics.Raycast(camRay, out floorHit, 200, floorMask))
        {
            transform.position = new Vector3(floorHit.point.x, transform.position.y, floorHit.point.z);
        }      
    }

    void Attack()
    {
        timer += Time.deltaTime;

        bulletPos = new Vector3(Random.Range(-10f, 10f) + this.transform.position.x, 10f ,Random.Range(-10f, 10f) + this.transform.position.z);

        if (timer >= 0.01f)
        {
            CannonBullet = Instantiate(CannonBullet, bulletPos, Quaternion.Euler(90, 0, 0)) as GameObject;
            CannonBullet = Resources.Load("Prefabs/Skill/CannonBullet") as GameObject;

            timer = 0;
        }
        
    }

    public bool Active()
    {
        return active;
    }
}
