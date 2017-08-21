using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

	GameObject loading;
	Animator am;

	IEnumerator TransitionTitle(){

		// シーン遷移のトリガーセット
		am.SetTrigger ("trans");

		// アニメーション終了待ち
		yield return new WaitForSeconds (1.5f);

		// シーンロード
		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		loading = GameObject.Find ("SceneTrans_02");
		//loading.SetActive (false);
		am = loading.GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		// タップされたらモードセレクトへシーン遷移
		if (Input.anyKeyDown) {
			StartCoroutine (TransitionTitle ());
		}
	}
		
}
