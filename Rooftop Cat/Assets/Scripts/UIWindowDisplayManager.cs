using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIWindowDisplayManager : MonoBehaviour
{
    public GameObject companyLogo_GO;

    public void CoroutineUITransitions(GameObject ui_GO, float time)
    {
        StartCoroutine(EnableGameObjectCoroutine(ui_GO, time));
    }

    IEnumerator EnableGameObjectCoroutine(GameObject _GO, float time)
    {
        yield return new WaitForSeconds(time);
        _GO.SetActive(true);
    }

    public void FadeUITransition(GameObject UIElement, bool hasBackwardsTransition, float fading, float time, float newFading, float newTime)
    {
        if (hasBackwardsTransition == true)
        {
            UIElement.GetComponent<SpriteRenderer>().material.DOFade(fading, time).OnComplete(() =>
            {
                UIElement.GetComponent<SpriteRenderer>().material.DOFade(newFading, newTime);
            });
        }
        else
        {
            UIElement.GetComponent<SpriteRenderer>().material.DOFade(fading, time);
        }
    }

    public void SelectNextWindow()
    {

    }

    public void SelectPreviousWindow()
    {

    }



}
