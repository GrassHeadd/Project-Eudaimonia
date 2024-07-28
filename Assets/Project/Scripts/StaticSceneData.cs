using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSceneData : MonoBehaviour
{
    public int LastDeathSceneIndex = -1;
    private static StaticSceneData instance;
    public int LevelIndicator= -1;
 
    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Debug.Log("destroyed game object singleton");
            Destroy(gameObject);
        }
        
    }
    
    public void setLevelIndicator(int newLevel) {
        LevelIndicator = newLevel;
    }
}
