using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBGM : MonoBehaviour {

	public static bool DontDestroyEnabled = true;

	// Use this for initialization
	void Start () {

		if (DontDestroyEnabled) {
			// Sceneを遷移してもオブジェクトが消えないようにする
			DontDestroyOnLoad (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
