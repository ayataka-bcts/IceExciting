using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

	GameObject background_1;
	GameObject background_2;

	// Use this for initialization
	void Start () {
		background_1 = GameObject.Find ("background_1");
		background_2 = GameObject.Find ("background_2");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-0.6f, -0.6f, 0);
//		if (transform.position.y < -1358.0f ) {
//			transform.position = new Vector3 (-1358f, 0, 0);
//		}
	}
}
