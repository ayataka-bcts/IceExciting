using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCall : MonoBehaviour {

	public static bool _flag;

	IEnumerator StartGame(){

		this.gameObject.GetComponent <Animator>().SetTrigger ("connect");

		yield return new WaitForSeconds (4.2f);

		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		_flag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (BluetoothManager.send_set == true) {
			if (_flag == false) {
				StartCoroutine (StartGame ());
				_flag = true;
			}
		}
	}
}
