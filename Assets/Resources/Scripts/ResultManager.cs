using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

	public GameObject win;
	public GameObject lose;

	// タイトルシーンへ遷移
	public void BackTitle(){
		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		win.SetActive (false);
		lose.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		// 勝者の場合
		if (GameManager.winner == Server_or_Client.role) {
			win.SetActive (true);
		}
		// 敗者の場合
		else if (GameManager.loser == Server_or_Client.role) {
			lose.SetActive (true);
		}
	}
}
