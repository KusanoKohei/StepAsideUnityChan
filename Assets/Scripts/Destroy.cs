using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    //メインカメラを入れる変数
    //private GameObject myCamera;
    Camera cam;
    //GameObject mainCamObj;

	// Use this for initialization
	void Start () {
        //シーン中のメインカメラオブジェクトを取得
        //myCamera = GameObject.Find("Main Camera");
        cam = Camera.main;  //MainCameraが一台の場合
        //mainCamObj = Camera.main.gameobject;  Camera.mainは「MainCamera」のTagがついたCameraコンポーネントを裏側で取得する。Tagを変えてしまうと機能しないので注意
	}
	
	// Update is called once per frame
	void Update () {
        //Destroy.csをアタッチしたオブジェクトが、メインカメラの背後に回ると自壊する
        if (gameObject.transform.position.z < cam.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
