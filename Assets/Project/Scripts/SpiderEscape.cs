using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEscape : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        Debug.Log("triggered");
        Debug.Log("tag of other: " + other.tag);
        Debug.Log("name of other: " + other.gameObject.name);
        if(other.tag == "Spider") {
            Debug.Log("tagged");
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<SpiderScript>().spidersLeft();
        }
    }
}
