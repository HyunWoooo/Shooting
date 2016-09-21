using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public enum State{
        DEFALUT = 0,

    }

    public Transform target;
    public float dist;
    public float hight;
    public float smooth = 100f;

    PlayerSkill playerSkill;
    InputManager inputManager;

    //Vector3 offset;
    Vector3 turning;

    int state = 0;

	// Use this for initialization
	void Start () {

        playerSkill = GameObject.Find("Player").GetComponent<PlayerSkill>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        //offset = transform.position - target.position;

        //Cursor.visible = false;
        GetComponent<CameraMotionBlur>().enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        
        if(playerSkill.AccelS())
            GetComponent<CameraMotionBlur>().enabled = true;
        else
            GetComponent<CameraMotionBlur>().enabled = false;


        CameraState();

        switch (state)
        {
            case 0:
                DefaultCamera();
                break;
            case 1:
                CannonCamera();
                break;
            default:
                break;

        }

        

    }

    void CameraState()
    {
        if(inputManager.Camera_State() == 1)
        {
            state = 1;
        }
        else if(inputManager.Camera_State() == 0)
        {
            state = 0;
        }
    }

    void DefaultCamera()
    {
        Quaternion rot = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, 0f); // 오일러각 강체의 놓인 방향을 알아내기위함.

        Vector3 targetCamPos = target.position - (rot * Vector3.forward * dist) + (Vector3.up * hight);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooth * Time.deltaTime);

        transform.LookAt(target.position);
    }

    void CannonCamera()
    {
        Vector3 targetCamPos = new Vector3(0f,90f,0f);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooth * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90f,0f,0f), smooth * Time.deltaTime);
    }

    void Turning()
    {
        //float currYAngle = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y, smooth * Time.deltaTime);
       // Quaternion rot = Quaternion.Euler
       // Vector3 turning = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        //transform.LookAt(target.position);
        //transform.RotateAround(target.position, Vector3.up, transform.rotation.z);
    }
}
