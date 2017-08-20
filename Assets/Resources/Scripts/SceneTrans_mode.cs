using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrans_mode : MonoBehaviour {

	// <summary>
	// これが居続けるとボタンをタップできないので、自壊機能を設定しておく
	// </summary>
	IEnumerator DestroyMyself(){

		yield return new WaitForSeconds (1.5f);

		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyMyself ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
