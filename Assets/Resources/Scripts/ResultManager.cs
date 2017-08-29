using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

	public GameObject win;
	public GameObject lose;

	public Text _name;

	public Ranking1Manager rm;

	AudioSource _audio;

	// タイトルシーンへ遷移
	public void BackTitle(){

		_audio.PlayOneShot (_audio.clip);

		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		win.SetActive (false);
		lose.SetActive (false);

		_name.text = Server_or_Client.user_name;
		_audio = this.gameObject.GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		// 勝者の場合
		if (GameManager.winner == Server_or_Client.role) {
			win.SetActive (true);
			//rm.SaveRanking (Server_or_Client.user_name, 1);
		}
		// 敗者の場合
		else if (GameManager.loser == Server_or_Client.role) {
			lose.SetActive (true);
		}
	}
}
