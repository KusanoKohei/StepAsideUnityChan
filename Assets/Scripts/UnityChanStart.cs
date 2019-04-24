using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanStart : MonoBehaviour {

    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    public AudioClip cheer;
    //タイトルでの声
    private AudioSource univoice;

	// Use this for initialization
	void Start () {

        //Animatorコンポネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //Audioコンポーネントを取得
        univoice = GetComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().PlayOneShot(cheer);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
