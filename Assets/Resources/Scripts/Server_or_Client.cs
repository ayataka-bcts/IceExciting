using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Server_or_Client : MonoBehaviour {

	public static string role;						// サーバかクライアントか
	public static string device_name;				// 接続相手のデバイス名
	public static string user_name;					// ランキング部分のユーザ名

	public Dropdown device_list;
	public InputField user_input;
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

		user_name = user_input.text;

		if (_text.text == "Server") {
			role = "server";
		} else if (_text.text == "Client") {
			role = "client";
			device_name = device_list.captionText.text;
		} 
	}
}
