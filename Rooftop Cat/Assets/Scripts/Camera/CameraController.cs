using System;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public ParallaxScroll parallaxScroll;
    public Transform playerTransform;
    public float scrollSpeed;
    public float cameraOffset;

    public Action CameraOutOfBoundsDelegate;

    public void Start()
    {
        var camera = gameObject.GetComponent<Camera>();
        //parallaxScroll.Initialize(camera);
        //parallaxScroll.SetParallaxScrollLayersSpeed();
    }

    public void Update()
    {
        ScrollCamera();
        //parallaxScroll.RunParallaxScrollEffect();
    }

    void ScrollCamera()
    {
        float cameraHeight = Mathf.Clamp(playerTransform.position.y,-6.0f,4.0f);
        if (playerTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        transform.position += new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);
        if (transform.position.x > playerTransform.position.x)
        {
            CameraOutOfBoundsDelegate?.Invoke();
        }
    }






}
