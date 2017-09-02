using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneload : MonoBehaviour {

	IEnumerator DestroyMyself(){
		yield return new WaitForSeconds (2.0f);

		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyMyself ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
