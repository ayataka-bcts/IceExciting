using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerIce : MonoBehaviour {

	public static int answer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.rotation = Quaternion.Euler (0, 0, answer);
	}
}
