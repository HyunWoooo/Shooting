using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

    enum Jump_State
    {
        DEFAULT = 0,
        MOVE,
        JUMPING,
        NOT
    };

    PlayerSkill playerSkill;
    PlayerBuff playerBuff;

    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;

    int floorMask;
    float camRayLength = 100f;

    bool playerJump;
    int jumpState;
    bool contact;

    bool walking;

    float timer;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerSkill = GetComponent<PlayerSkill>();
        playerBuff = GetComponent<PlayerBuff>();

        floorMask = LayerMask.GetMask("Floor");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
	    
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKeyDown(KeyCode.Space) && !playerJump)
            playerJump = true;

        Move(h, v);

        Turning();

        Animating(h, v);
    }

    void Move(float h, float v)
    {
        if (9.2f <= playerSkill.AccelCool && playerSkill.AccelCool < 10)
        {
            speed = 20f;
        }
        else
        {
            if (playerBuff.SPD_BUFF())
            {
                speed = 10f;
            }
            else
                speed = 5f;
        }


        if (h == 0 && v == 0)
            movement.Set(0, 0, 0);

        if (h < 0)
            movement = -transform.right;
        else if (h > 0)
            movement = transform.right;
        if (v < 0)
            movement = -transform.forward;
        else if (v > 0)
            movement = transform.forward;

        if (playerJump == true)
        {
            timer += Time.deltaTime;

            if (!contact)
                jumpState = (int)Jump_State.JUMPING;
            else if (h == 0 && v == 0 && jumpState != (int)Jump_State.JUMPING)
                jumpState = (int)Jump_State.DEFAULT;
            else if (jumpState != (int)Jump_State.JUMPING)
                jumpState = (int)Jump_State.MOVE;

            if (contact && timer>1f)
            {
                playerJump = false;
                jumpState = (int)Jump_State.NOT;
                timer = 0;
            }

            switch (jumpState)
            {
                case (int)Jump_State.DEFAULT:
                    transform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1f, 0f) * 800);
                    jumpState = (int)Jump_State.JUMPING;
                    break;
                case (int)Jump_State.MOVE:
                    transform.GetComponent<Rigidbody>().AddForce((new Vector3(0f, 1f, 0f) + movement) * 800);
                    jumpState = (int)Jump_State.JUMPING;
                    return;
                case (int)Jump_State.JUMPING:
                    return;
            }
        }

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Vector3 turning = new Vector3(0, Input.GetAxis("Mouse X"), 0);

        turning = turning + new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        if (10 <= transform.localEulerAngles.x && transform.localEulerAngles.x <= 180)
            transform.Rotate(new Vector3(-1,0,0) * 3);

        if (180 <= transform.localEulerAngles.x && transform.localEulerAngles.x <= 340)
            transform.Rotate(new Vector3(1, 0, 0) * 3);

        transform.Rotate(turning * 3);
    }

    void Animating(float h, float v)
    {
        walking = h != 0 || v != 0f;

        anim.SetBool("IsWalking", walking);
    }

    void OnCollisionEnter()
    {
        contact = true;
    }

    void OnCollisionEixt()
    {
        contact = false;
    }

    public bool GetWalking()
    {
        return walking;
    }
}
