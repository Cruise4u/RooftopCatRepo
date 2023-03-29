using System;
using UnityEngine;

public class GroundCollisionController : MonoBehaviour
{
    public string buildingTag;
    public Action<bool> SetIsGroundedDelegate;

    public void OnEnable()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(buildingTag))
        {
            SetIsGroundedDelegate.Invoke(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(buildingTag))
        {
            SetIsGroundedDelegate.Invoke(false);
        }
    }
}