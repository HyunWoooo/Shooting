using UnityEngine;
using System.Collections;

public class EnemyUI : MonoBehaviour
{

    Transform MainCamera;

    // Use this for initialization
    void Start()
    {

        MainCamera = GameObject.Find("Main Camera").transform;


    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(MainCamera.position);

    }
}
