using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private int speedOfTransition = 10;
    public static SceneTransitionManager singleton;

    private void Awake()
    {
        if (singleton && singleton != this)
            Destroy(singleton);

        singleton = this;
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
       while(Background.color.a < 1) {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, Background.color.a + speedOfTransition/255f);
            Debug.Log(Background.color.a);
            yield return new WaitForFixedUpdate();
        }

        //Launch the new scene
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        Debug.Log("go to scene async is called");
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        //Launch the new scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(Background.color.a < 1) {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, Background.color.a + speedOfTransition/255f);
            Debug.Log(Background.color.a);
            yield return new WaitForFixedUpdate();
        }

        operation.allowSceneActivation = true;
    }
}
