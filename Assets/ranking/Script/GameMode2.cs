using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode2 : MonoBehaviour
{
    public InputField playerName;
    public Text scoreText;
    public int score = 0;
    public Button saveScoreButton;
    public Button showRankingButton;

    public Text message;


    private void Start()
    {

        //NCMB SDK はWebGLに対応していないため、WebGLビルドしたときにはランキング機能をオフにしてください//
#if UNITY_WEBGL
        showRankingButton.interactable = false;
        saveScoreButton.interactable = false;
#else
        showRankingButton.interactable = false;
        saveScoreButton.interactable = true;
#endif

    }

    public void Tap()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ShowRanking()
    {
        //showRankingButton.interactable = false;//連打防止//
        SceneManager.LoadScene("MenuScene");
    }

    public void SavePlayerScore()
    {
        saveScoreButton.interactable = false;//連打防止//
        Ranking2Manager.Instance.SaveRanking(playerName.text, score, SetEnableSHowRankingButton);
    }

    void SetEnableSHowRankingButton()
    {
        //自分のスコアを保存すると、ShowRankingButtonが押せるようになる//
        showRankingButton.interactable = true;
        message.text = "Your score saved!";
    }
}