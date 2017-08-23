using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushRankigButton()
    {
        SceneManager.LoadScene("RankingScene");
    }
    public void PushMode1Button()
    {
        SceneManager.LoadScene("GameMode1");
    }

    public void PushMode2Button()
    {
        SceneManager.LoadScene("GameMode2");
    }

}
