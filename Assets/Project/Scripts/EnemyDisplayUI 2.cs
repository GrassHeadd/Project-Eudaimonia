using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyDisplayUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image Background;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private int speedOfTransition = 10;

    public void updateCountText(int count) {
        countText.text = "Enemies Left: " + count;
    }

    public void updateTimeText(float time) {
        countText.text = "Time Left: " + time;
    }

    public IEnumerator endGameTask() {
        while(Background.color.a < 1) {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, Background.color.a + speedOfTransition/255f);
            Debug.Log("waiting friend");
            Debug.Log(Background.color.a);
            yield return new WaitForFixedUpdate();
        }
            SceneManager.LoadSceneAsync("Start Scene");
    }

    public void endGame() {
        Debug.Log("endGame getting called");
        StartCoroutine(endGameTask());
    }
}
