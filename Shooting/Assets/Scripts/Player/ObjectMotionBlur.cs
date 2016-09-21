using UnityEngine;
using System.Collections;

public class ObjectMotionBlur : MonoBehaviour {

    Shader MotionBluer;
    Material material;

    Matrix4x4 ModelViewProjectionMat;
    Matrix4x4 PrevModelViewProjectionMat;

    Camera camera;

    RenderTexture accumTexture;

    float timer;

	// Use this for initialization
	void Start () {

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        MotionBluer = Resources.Load("Shader/SurMotionBlur") as Shader;

        transform.GetComponent<Renderer>().material.shader = MotionBluer;

        material = transform.GetComponent<Renderer>().material;

        PrevModelViewProjectionMat = transform.localToWorldMatrix * camera.worldToCameraMatrix * camera.projectionMatrix;

    }
	
	// Update is called once per frame
	void Update () {

        

        if (transform.GetComponent<Renderer>().material.shader == MotionBluer)
        {
            Matrix4x4 P = GL.GetGPUProjectionMatrix(camera.projectionMatrix, false);
            Matrix4x4 V = camera.worldToCameraMatrix;
            Matrix4x4 M = transform.localToWorldMatrix;
            Matrix4x4 MVP = P * V * M;

            ModelViewProjectionMat = camera.projectionMatrix * camera.worldToCameraMatrix;
            Matrix4x4 inverseViewProjection = ModelViewProjectionMat.inverse;

            material.SetMatrix("_uModelViewProjectionMat", MVP);
            material.SetMatrix("_uPrevModelViewProjectionMat", PrevModelViewProjectionMat);

            timer += Time.deltaTime;

            if(timer > 0.1f)
            {
                PrevModelViewProjectionMat = ModelViewProjectionMat;
                timer = 0;
            }

            
        }
    }
}
