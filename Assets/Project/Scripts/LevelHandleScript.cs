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
        int levelIndex = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator++; //penis
        SceneManager.LoadSceneAsync(levelIndex >= 4 ? 2 : 1);
    }
}
