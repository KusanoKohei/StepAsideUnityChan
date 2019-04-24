using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour {

    public AudioClip bgm;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().PlayOneShot(bgm);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
