using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class solo_managaer : MonoBehaviour {

	private Text time_text;					// 時間表示用
	public static bool _OnStart;			// ゲームがスタートしているかどうか
	public static bool _Change;				// 食べる時間とかたむける時間の切り替え

	[SerializeField]
	private GameObject finish_button;		// 終了ボタンを動かす用
	[SerializeField]
	private GameObject eat_timer;			
	[SerializeField]
	private GameObject start_call;			
	[SerializeField]
	private GameObject cutin;

	public Text texd;

	public GameObject canvasObject;

	string status;

	bool eat_trigger;

	int _sensor1;
	int _sensor2;
	int[] _sensor;
	int count;

	int angle;
	int diff;
	int pre_time; 

	// 時間計測のためのストップウォッチクラス
	private System.Diagnostics.Stopwatch sw;

	// Use this for initialization
	void Start () {

		// 変数の初期化
		_OnStart = false;
		_sensor = new int[5];
		status = "perform";
		angle = 0;
		eat_trigger = false;
		diff = 0;
		count = 0;

		// テキストコンポーネントを取得
		time_text = GameObject.Find("Total_Time").GetComponent<Text>();

		//Stopwatchのインスタンス作成
		sw = new System.Diagnostics.Stopwatch(); 

		StartCoroutine (StartGame ());

		pre_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		// ゲーム開始状態なら経過時間を表示する
		if (_OnStart == true) {
			
			// 時間を取得
			var time = sw.Elapsed;

			// 表示形式を整えて時間表示
			time_text.text = time.Minutes.ToString("D2") + ":" + 
				time.Seconds.ToString("D2") + "." + time.Milliseconds.ToString("D3");

			texd.text = status.ToString ();

			if (status == "perform") {

				// 指定の角度を生成
				if (angle == 0) {
					angle = GenerateTargetAngle ();
				}

				// 加速度計の値を取得
				GetAccele (BluetoothManager_solo._fromDevice);

				// 値の判定
				if (CheckAngle (_sensor, angle)) {

					// 交代処理
					status = "eat";

				}

			} else if (status == "eat") {

				angle = 0;

				// 差分の時間を算出
				if (eat_trigger == false) {
					diff = time.Seconds - pre_time;

					eat_trigger = true;

					// 食べられる時間のセット
					TastingTimer.eat_time = EvaluateEating (diff);

					//GameObjectを生成、生成したオブジェクトを変数に代入
					GameObject prefab = (GameObject)Instantiate (eat_timer); 

					//Canvasの子要素として登録する 
					prefab.transform.SetParent (canvasObject.transform, false);

				}

				// 交代処理
				if (_Change == true) {
					status = "perform";
					eat_trigger = false;
					_Change = false;
				}
			}
		}
	}

	// 初めのアニメーション
	IEnumerator StartGame(){

		start_call.SetActive (true);

		start_call.GetComponent <Animator>().SetTrigger ("connect");

		yield return new WaitForSeconds (5.0f);

		Destroy (start_call);

		_OnStart = true;

		sw.Start ();
	}

	// デバイスからのメッセージを分解してintに格納
	void GetAccele(string str){

		string[] receive = str.Split (',');

		_sensor1 = Mathf.FloorToInt (float.Parse(receive [0]));
		_sensor2 = Mathf.FloorToInt (float.Parse(receive [1]));

		_sensor [count % 5] = _sensor2;
		count++;

	}

	// 時間に応じて食べられる時間を算出
	float EvaluateEating(int time){

		// 時間に応じて食べられる時間を設定
		if (0 < time && time <= 2) {
			return 0.001f;
		} else if (2 < time && time <= 5) {
			return 0.005f;
		} else {
			return 0.01f;
		}
	}

	// 乱数をある程度綺麗な数値に整える
	int RangeValueMapping(int value){

		if (0 < value) {
			if (0 < value && value < 15) {
				value = 0;
			} else if (15 < value && value < 30) {
				value = 30;
			} else if (30 < value && value < 45) {
				value = 45;
			} else if (45 < value && value < 60) {
				value = 60;
			} else if (60 < value && value < 75) {
				value = 75;
			} else if (75 < value && value < 90) {
				value = 90;
			} else if (90 < value && value < 105) {
				value = 105;
			} else if (105 < value && value < 120) {
				value = 120;
			} else if (120 < value && value < 135) {
				value = 135;
			} else if (135 < value && value < 150) {
				value = 150;
			} else if (150 < value && value < 165) {
				value = 165;
			} else if (165 < value && value < 180) {
				value = 170;
			}
		}

		return value;

	}

	// ランダムで指定する角度を生成する
	int GenerateTargetAngle(){

		// 0度から180度までの間で乱数発生
		// [Todo]綺麗な数値の乱数を作れる関数あるかも
		int rnd = Random.Range (15, 180);

		// 適度な数値にマッピングし直す
		int ang = RangeValueMapping (rnd);

		// 答えの視覚化のために値を更新しておく
		AnswerIce.answer = ang;

		return ang;
	}

	// 角度の判定(引数：センサ値、指定の角度)
	bool CheckAngle(int[] sensor, int target){

		// [要調整]いくつかの誤差を許容してもいいかも
		if (sensor[0] == target && sensor[1] == target && sensor[2] == target && sensor[3] == target && sensor[4] == target) {
			// 横の傾き以外を検知しないように、かつ左右のかたむけを検知できるように
			if (-45 <= _sensor1 && _sensor1 < 45) {
				return true;
			} 
			return false;
		} else {
			return false;
		}
	}

	// フィニッシュボタンを押した時
	public void FinishSolo(){

		// 計測終了
		sw.Stop();

	}

	public void LoadScene(string name){
		SceneManager.LoadScene (name);
	}
}
