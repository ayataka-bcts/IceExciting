using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class solo_managaer : MonoBehaviour {

	private Text time_text;					// 時間表示用
	private bool _OnStart;					// ゲームがスタートしているかどうか
	[SerializeField]
	private GameObject finish_button;		// 終了ボタンを動かす用
	[SerializeField]
	private GameObject result_score;		// スコア画面
	[SerializeField]
	private GameObject debug_pannel;		// デバッグ用の画面

	// 時間計測のためのストップウォッチクラス
	private System.Diagnostics.Stopwatch sw;

	// Use this for initialization
	void Start () {

		// 変数の初期化
		_OnStart = false;

		// テキストコンポーネントを取得
		time_text = GameObject.Find("Total_Time").GetComponent<Text>();

		//Stopwatchのインスタンス作成
		sw = new System.Diagnostics.Stopwatch(); 
	}
	
	// Update is called once per frame
	void Update () {

		// デバッグモードに切り替え
		if (Input.GetKey (KeyCode.D)) {
			
		}

		// ゲーム開始状態なら経過時間を表示する
		if (_OnStart == true) {
			// 時間を取得
			var time = sw.Elapsed;

			// 表示形式を整えて時間表示
			time_text.text = time.Minutes.ToString("D2") + ":" + 
				time.Seconds.ToString("D2") + "." + time.Milliseconds.ToString("D3");
		}
	}


	// スタートボタンを押した時
	public void StartSolo(){

		// 始まった状態にセットする
		_OnStart = true;

		// 終了ボタンを見えるようにする
		VisibleOnDisplay (finish_button);

		// 計測開始
		sw.Start ();
	}

	// フィニッシュボタンを押した時
	public void FinishSolo(){

		// 計測終了
		sw.Stop();

		// スコアを表示
		GameObject.Find ("score").GetComponent<Text> ().text = time_text.text;

		// スコア、ランキングを見えるようにする
		VisibleOnDisplay(result_score);

		// ランキング表示
	}

	// 画面上に見えるようにする
	void VisibleOnDisplay(GameObject obj){
		var tmp = obj.GetComponent<RectTransform> ().localPosition.y;
		obj.GetComponent<RectTransform> ().localPosition = new Vector3(0.0f, tmp, 0.0f);
	}

	public void LoadScene(string name){
		SceneManager.LoadScene (name);
	}
}
