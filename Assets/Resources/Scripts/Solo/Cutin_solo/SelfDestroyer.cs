using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestroyer : MonoBehaviour {

	public GameObject timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timer.GetComponent <Image>().fillAmount == 0) {
			Destroy (this.gameObject);
			solo_managaer._Change = true;
		}
	}
}
