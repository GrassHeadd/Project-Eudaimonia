using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeGameWinCondition : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
        Debug.Log("triggered");
        Debug.Log("tag of other: " + other.tag);
        Debug.Log("name of other: " + other.gameObject.name);
        if(other.tag == "Body") {
            Debug.Log("tagged");
            playerWin();
        }
    }

    public void playerWin() {
        Debug.Log("playerWin called");
        GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync("WinScene");
    }
}
