using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Server_or_Client : MonoBehaviour {

	public static string role;
	public static string device_name;

	public Dropdown dd;
	Text _text;

	public Animator am;

	public void BackModeSelect(){

		StartCoroutine (TransitionRanking ());
	}

	IEnumerator TransitionRanking(){

		// シーン遷移のトリガーセット
		am.SetTrigger ("trans");

		// アニメーション終了待ち
		yield return new WaitForSeconds (1.5f);

		// シーンロード
		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		_text = GameObject.Find ("debug_status").GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_text.text == "Server") {
			role = "server";
		} else if (_text.text == "Client") {
			role = "client";
			device_name = dd.captionText.text;
		} 
	}
}
