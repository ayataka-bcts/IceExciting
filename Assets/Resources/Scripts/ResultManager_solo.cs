using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager_solo : MonoBehaviour {

	public Text score_text;

	public Text _name;

	public static string score;

	public Ranking1Manager rm;

	AudioSource _audio;

	// タイトルシーンへ遷移
	public void BackTitle(){

		_audio.PlayOneShot (_audio.clip);

		MenuBGM.DontDestroyEnabled = false;

		SceneManager.LoadScene ("mode_select");
	}

	// Use this for initialization
	void Start () {
		_audio = GameObject.Find("Button").GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		score_text.text = score;

	}
}
