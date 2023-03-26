using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    private Transform cam;
    private Vector3 previousCamPos;

    public Transform[] backgrounds;
    public float[] parallaxScales;
    public float smoothing = 1f;

    public void Initialize(Camera camera)
    {
        cam = camera.transform;
        previousCamPos = cam.position;
    }

    public void SetParallaxScrollLayersSpeed()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    public void RunParallaxScrollEffect()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }

}
