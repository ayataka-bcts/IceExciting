using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TastingTimer : MonoBehaviour {

	Image timer;
	public static float eat_time;

	// Use this for initialization
	void Start () {
		timer = this.GetComponent <Image> ();
		timer.fillAmount = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer.fillAmount -= eat_time;
		if (timer.fillAmount < 0.20f) {
			timer.color = Color.red;
		}
	}
}
