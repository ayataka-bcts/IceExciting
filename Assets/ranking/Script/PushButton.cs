using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushButton : MonoBehaviour {

   Text scoreBoardText;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}


   
    public void PushRankig1Button()
    {
		// ランキング情報の取得
        Ranking1Manager.Instance.FetchRanking();
		// テキスト形式にして取得
        Ranking1Manager.Instance.GetRanking1ByText();

        SceneManager.LoadScene("GameMode1RankingBoard");
    }

    public void PushRankig2Button()
    {
        Ranking2Manager.Instance.FetchRanking();
        Ranking2Manager.Instance.GetRanking2ByText();

        SceneManager.LoadScene("GameMode2RankingBoard");
    }

    public void PushBackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
