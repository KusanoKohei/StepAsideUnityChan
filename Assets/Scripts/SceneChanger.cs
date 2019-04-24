using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        SceneChange();
	}

    public void SceneChange()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
