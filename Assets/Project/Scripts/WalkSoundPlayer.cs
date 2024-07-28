using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundPlayer : MonoBehaviour
{
    public AudioSource src;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject player;
    Vector3 oldPos;
    // Start is called before the first frame update

    void Start() {
        src.clip = audioClip;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("curr: " + player.transform.position);
        Debug.Log("old: " + oldPos);
        if(player.transform.position != oldPos /*&& is audioClip playing*/) {
            Debug.Log("play called");
            src.Play();
        } else {
            src.Stop();
        }
        oldPos = player.transform.position;
    }
}
