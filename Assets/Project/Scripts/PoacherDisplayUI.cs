using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PoacherDisplayUI : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private int speedOfTransition = 10;

    public void updateCountText(int count) {
        countText.text = "Hits Left: " + count;
    }

    public IEnumerator endGameTask() {
        while(Background.color.a < 1) {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b, Background.color.a + speedOfTransition/255f);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("Win called");
        SceneManager.LoadSceneAsync("WinScene");
    }

    public void endGame() {
        Debug.Log("endGame getting called");
        StartCoroutine(endGameTask());
    }
}
