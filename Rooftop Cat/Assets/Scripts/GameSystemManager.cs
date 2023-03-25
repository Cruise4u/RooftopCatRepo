using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystemManager : MonoBehaviour
{
    public SceneManager sceneManager;
    public UIWindowDisplayManager uiWindowDisplayManager;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        RunFirstScreenDisplayCoroutine();
    }

    public void RunFirstScreenDisplayCoroutine()
    {
        StartCoroutine(FirstScreenDisplayCoroutine(5f));
    }

    public IEnumerator FirstScreenDisplayCoroutine(float time)
    {
        //Fade Object initially from Alpha 0 -> 1
        uiWindowDisplayManager.FadeUITransition(uiWindowDisplayManager.companyLogo_GO,true,1,3,0,1.5f);
        // The wait time in the coroutine should be the sum of both fading values on the method above + 0.25f
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }




    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }









}
