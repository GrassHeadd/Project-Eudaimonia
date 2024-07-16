using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharkBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Ammo") {
            Debug.Log("rock collided");
            //GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync("DeathScene");
        }
    }
}
