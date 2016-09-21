using UnityEngine;
using System.Collections;

public class CameraMotionBlur : MonoBehaviour
{
    static float MAX_RADIUS = 10.0f;

    Camera _camera;
    Shader shader;
    Material motionBlurMaterial = null;

    Matrix4x4 currentViewProjMat;
    Matrix4x4 prevViewProjMat;

    float velocityScale = 0.375f;

    int prevFrameCount;

    // Use this for initialization
    void Start()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;

        shader = Shader.Find("Unlit/MotionBlur2");
        Material m = null;
        m = new Material(shader);
        m.hideFlags = HideFlags.DontSave;
        motionBlurMaterial = m;

        _camera = GetComponent<Camera>();
        
        CalculateViewProjection();
        Remember();
    }

    void CalculateViewProjection()
    {
        Matrix4x4 viewMat = _camera.worldToCameraMatrix;
        Matrix4x4 projMat = GL.GetGPUProjectionMatrix(_camera.projectionMatrix, true);
        currentViewProjMat = projMat * viewMat;
    }

    void Remember()
    {
        prevViewProjMat = currentViewProjMat;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        RenderTexture velBuffer = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);

        CalculateViewProjection();

        Matrix4x4 invViewPrj = Matrix4x4.Inverse(currentViewProjMat);

        motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", prevViewProjMat * invViewPrj);

        motionBlurMaterial.SetFloat("_VelocityScale", velocityScale);

        motionBlurMaterial.SetTexture("_VelTex", velBuffer);

        Graphics.Blit(source, velBuffer, motionBlurMaterial, 0);

        Remember();

        Graphics.Blit(source, destination, motionBlurMaterial, 1);

        RenderTexture.ReleaseTemporary(velBuffer);

    }
}