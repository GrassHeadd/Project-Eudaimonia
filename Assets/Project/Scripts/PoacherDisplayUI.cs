using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoacherDisplayUI : MonoBehaviour
{
    int enemiesLeft = 4;

    [SerializeField] private Image Background;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private int speedOfTransition = 10;
    // Start is called before the first frame update
    public void updateCountText() {
        countText.text = "Hits Left: " + enemiesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
