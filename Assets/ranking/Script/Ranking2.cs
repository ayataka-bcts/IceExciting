using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ranking2 : MonoBehaviour
{

   public Text scoreBoardText;
   public Text playerCounter;

    //static private int gameMode = 2;

    // Use this for initialization
    void Start()
    {

        scoreBoardText.text = Ranking2Manager.Instance.GetRanking2ByText();

        //QuickRanking.Instance.FetchPlayerCount(SetPlayerCounter);
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoardText.text = Ranking2Manager.Instance.GetRanking2ByText();
    }


    void SetPlayerCounter()
    {
        playerCounter.text = "Total Player Count: " + Ranking2Manager.Instance.PlayerCount.ToString();
    }

    public void BackToRanking()
    {
        
        SceneManager.LoadScene("RankingScene");
    }
}
