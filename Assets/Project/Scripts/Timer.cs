using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] float time = 200f;
    [SerializeField] private EnemyDisplayUI enemyDisplayUI;
    SnakeScript snakeScript = new SnakeScript();

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(timesUp(time));
    }

    public IEnumerator timesUp(float t) {
       while(time > 0) {
        yield return new WaitForSeconds(1);
        time--;
        enemyDisplayUI.updateTimeText(time);
       }
        GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync("DeathScene");
    }
}
