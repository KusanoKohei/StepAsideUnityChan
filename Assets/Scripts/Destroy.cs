using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    //メインカメラを入れる変数
    private GameObject myCamera;

	// Use this for initialization
	void Start () {
        //シーン中のメインカメラオブジェクトを取得
        myCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        //Destroy.csをアタッチしたオブジェクトが、メインカメラの背後に回ると自壊する
        if (gameObject.transform.position.z < myCamera.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
