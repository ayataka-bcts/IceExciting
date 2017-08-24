using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

	Text score_board;

	[SerializeField]
	private Text[] ranking_name;
	[SerializeField]
	private Text[] ranking_score;

	void DisplayRanking(string[] data){

		// 各データの配列に格納する
		for (int i = 0; i < data.Length/3; i++) {
			ranking_name[i].text = data [(3 * i) + 1];
			ranking_score[i].text = data [(3 * i) + 2];
		}

	}

	// 戻るボタンを押した時の処理
	public void BackButton(){
		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		//score_board.text = Ranking1Manager.Instance.GetRanking1ByText();
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (Ranking1Manager.Instance.GetRanking1ByText());
		string str = Ranking1Manager.Instance.GetRanking1ByText ();
		string[] rank = str.Split (',');
		if (rank != null) {
			DisplayRanking (rank);
		}

	}
}
