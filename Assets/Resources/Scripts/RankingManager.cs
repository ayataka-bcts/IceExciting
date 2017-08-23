using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

	public Text score_board;

	// Use this for initialization
	void Start () {

		score_board.text = Ranking1Manager.Instance.GetRanking1ByText();
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (Ranking1Manager.Instance.GetRanking1ByText());
		score_board.text = Ranking1Manager.Instance.GetRanking1ByText();
	}
}
