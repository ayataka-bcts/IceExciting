using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mode_select : MonoBehaviour {

	Animator scene_trans;

	// Use this for initialization
	void Start () {
		scene_trans = GameObject.Find ("SceneTrans_02").GetComponent <Animator> ();
		scene_trans.Play ("transition");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectMode(string mode){
		SceneManager.LoadScene (mode);
	}
}
