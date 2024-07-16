using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEscape : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Spider") {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<SpiderScript>().spidersLeft();
        }
    }
}
