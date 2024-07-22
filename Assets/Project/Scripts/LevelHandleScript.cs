using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelHandleScript : MonoBehaviour
{
    public void Restart()
    {
        int sceneIndex = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex;
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void NextLevel() {
        //int sceneIndex = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex;
        int levelIndex = ++GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator;
        Debug.Log("level: " + levelIndex);
        if(levelIndex < 4) {
            SceneManager.LoadSceneAsync(1);
        } else if(levelIndex <= 8 && levelIndex >=4) {
            SceneManager.LoadSceneAsync(2);
        } else if(levelIndex > 8 && levelIndex < 13) {
                SceneManager.LoadSceneAsync(3);
        } else {
                SceneManager.LoadSceneAsync(0);
        }
    }
}
