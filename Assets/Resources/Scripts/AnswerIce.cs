using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerIce : MonoBehaviour {

	public static int answer;

	int _sensor1;
	int _sensor2;

	// デバイスからのメッセージを分解してintに格納
	void GetAccele(string str){

		string[] receive = str.Split (',');

		_sensor1 = Mathf.FloorToInt (float.Parse(receive [0]));
		_sensor2 = Mathf.FloorToInt (float.Parse(receive [1]));

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.rotation = Quaternion.Euler (0, 0, answer);
	}
}
