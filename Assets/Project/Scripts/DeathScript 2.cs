using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{

    public void Restart() {
         int yes = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex;
        SceneManager.LoadSceneAsync(yes);
    }

    public void Quit() {
        SceneManager.LoadSceneAsync(0);
    }
}
