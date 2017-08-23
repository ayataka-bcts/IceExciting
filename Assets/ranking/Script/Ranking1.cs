using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ranking1 : MonoBehaviour {

     public Text scoreBoardText;
     public Text playerCounter;

    // Use this for initialization
    void Start () {
        scoreBoardText.text = Ranking1Manager.Instance.GetRanking1ByText();

        //Ranking1Manager.Instance.FetchPlayerCount(SetPlayerCounter);
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoardText.text = Ranking1Manager.Instance.GetRanking1ByText();

    }

    void SetPlayerCounter()
    {
        playerCounter.text = "Total Player Count: " + Ranking1Manager.Instance.PlayerCount.ToString();
    }

    public void BackToRanking()
    {
        SceneManager.LoadScene("RankingScene");
    }
}
