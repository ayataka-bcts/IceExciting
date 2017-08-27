using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCall : MonoBehaviour {

	IEnumerator DestroyMyself(float time){
		
		yield return new WaitForSeconds (time);

		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent <Animator>().SetTrigger ("connect");

		StartCoroutine (DestroyMyself (4.2f));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
