using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTranser : MonoBehaviour {

	IEnumerator DestroyMyself(){

		yield return new WaitForSeconds (5.0f);

		Destroy (this.gameObject);
	}

	void Start(){
		StartCoroutine (DestroyMyself ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
