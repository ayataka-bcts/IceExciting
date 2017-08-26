using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mode_select : MonoBehaviour {

	Animator scene_trans_title;
	Animator scene_trans_ranking;

	// Use this for initialization
	void Start () {
		scene_trans_title = GameObject.Find ("SceneTrans_02").GetComponent <Animator> ();
		scene_trans_ranking = GameObject.Find ("SceneTrans_ranking").GetComponent <Animator> ();
		scene_trans_title.Play ("transition");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectMode(string mode){

		if (mode == "Ranking") {
			// ランキング情報の取得
			Ranking1Manager.Instance.FetchRanking();
			// テキスト形式にして取得
			Ranking1Manager.Instance.GetRanking1ByText();

			StartCoroutine (TransitionModeSelect (mode));
		}

		if (mode == "Versus_setting") {
			StartCoroutine(TransitionModeSelect (mode));
		}
			
	}

	IEnumerator TransitionModeSelect(string mode){

		// シーン遷移のトリガーセット
		scene_trans_ranking.SetTrigger ("trans");

		// アニメーション終了待ち
		yield return new WaitForSeconds (1.5f);

		// シーンロード
		SceneManager.LoadScene (mode);
	}
}
