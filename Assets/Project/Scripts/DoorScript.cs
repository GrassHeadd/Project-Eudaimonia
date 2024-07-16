using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    //positions
    [SerializeField] public Vector3 Position;
    //rotations
    [SerializeField] public Vector3 Rotation;
    public int diffLvl;
    void Start() {
        diffLvl = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator;
        Debug.Log("diff level: " + diffLvl);
        if(diffLvl == 1 || diffLvl == 2) {
            //set position to be closed
        }
    }

    // Update is called once per frame
    void FixedUpdate() {

    }
}
