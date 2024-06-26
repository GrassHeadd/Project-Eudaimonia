using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEscape : MonoBehaviour
{
    public GameObject other;

    void onTriggerEnter(Collider otherCollider) {
        Debug.Log("onTriggerEnter called");
        if(otherCollider.gameObject.name == "GrassLand") {
            Debug.Log("called");
            other.SetActive(false);
        }
    }
}
