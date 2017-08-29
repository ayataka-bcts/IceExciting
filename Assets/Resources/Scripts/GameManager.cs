using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// ESPから送られてくる値(1:仰角,2:水平角)
	private int _sensor1;
	private int _sensor2;

	// 判定の時に過去いくつかの数値を使用する
	private int[] _sensor;
	private int count;

	// ランダムに生成する角度(指定の角度)
	private int angle;

	// 勝者と敗者(serverかclientで判断。次のシーンで使う)
	public static string winner;
	public static string loser;

	// 今どちらの状態かを判断する変数
	public static string atack_or_defence;

	// 攻守交代のトリガー
	public static bool change_trigger;

	// 攻撃側の終了合図
	public static bool end_atack;

	// ゲーム終了の合図
	public static bool end_game;

	// カットイン用のオブジェクト
	public GameObject cutin;

	// 現在の攻守がどちらかを示すテキスト
	public Text a_or_d;

	public Text tx3;
	public Text tx4;

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
				value = 175;
			}
		}

		return value;

	}

	// ランダムで指定する角度を生成する
	int GenerateTargetAngle(){

		// 0度から180度までの間で乱数発生
		// [Todo]綺麗な数値の乱数を作れる関数あるかも
		int rnd = Random.Range (0, 180);

		// 適度な数値にマッピングし直す
		int angle = RangeValueMapping (rnd);

		// 答えの視覚化のために値を更新しておく
		AnswerIce.answer = angle;

		return angle;
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

	// ゲームが終了した時に押されるボタン(勝者が押す感じかな)
	public void EndGame(){

		winner = Server_or_Client.role;

		if (winner == "client") {
			loser = "server";
		} else if (winner == "server") {
			loser = "client";
		}

		end_game = true;
		SceneManager.LoadScene ("result");
	}

	// 攻守交代のときのアニメーション再生
	IEnumerator ChangeAnimation(){

		// カットインオブジェクトをアクティブ
		cutin.SetActive (true);

		// カットインのアニメーション再生
		cutin.GetComponent <Animator> ().SetTrigger ("cutin");

		yield return new WaitForSeconds(2.0f); 

		// カットインオブジェクトを非アクティブに
		cutin.SetActive (false);

	}

	// デバイスからのメッセージを分解してintに格納
	void GetAccele(string str){

		string[] receive = str.Split (',');

		_sensor1 = Mathf.FloorToInt (float.Parse(receive [0]));
		_sensor2 = Mathf.FloorToInt (float.Parse(receive [1]));

		_sensor [count % 5] = _sensor2;
		count++;

	}

	public void debugchange()
	{
		StartCoroutine (ChangeAnimation ());
		change_trigger = true;
	}

	// Use this for initialization
	void Start () {

		// 変数の初期化
		angle = 0;
		end_game = false;
		change_trigger = false;
		end_atack = false;
		cutin.SetActive (false);
		_sensor = new int[5];
		count = 0;

		// サーバかクライエントで先行後攻を決定
		if (Server_or_Client.role == "server") {
			atack_or_defence = "atack";
		} else if (Server_or_Client.role == "client") {
			atack_or_defence = "defence";
		}

	}
	
	// Update is called once per frame
	void Update () {

		//tx3.text = "1:" + _sensor1.ToString () + " 2:" + _sensor2.ToString ();
		//tx4.text = atack_or_defence;

		if (StartCall._flag == true) {
			// アイスを食べる側のとき
			if (atack_or_defence == "atack") {

				angle = 0;

				a_or_d.text = "食べろ!!";

				// 攻守交代
				if (end_atack == true) {
					// 順次実行で処理して欲しいのでコルーチンを使用
					StartCoroutine (ChangeAnimation ());
					change_trigger = true;
				}
			}
			// アイスを傾ける側のとき
			else if (atack_or_defence == "defence") {

				a_or_d.text = "傾けろ!!";

				// 指定の角度を生成
				if (angle == 0) {
					angle = GenerateTargetAngle ();
					GameObject.Find ("debug_2").GetComponent <Text> ().text = "angle:" + angle.ToString ();
				}

				// 加速度計の値を取得
				GetAccele (BluetoothManager._fromDevice);

				// 値の判定
				if (CheckAngle (_sensor, angle)) {
				
					// 順次実行で処理して欲しいのでコルーチンを使用
					StartCoroutine (ChangeAnimation ());
					change_trigger = true;
				}

			}

			// 通信によってゲームの終了を知ったら
			if (BluetoothManager.end_ok == true) {
				SceneManager.LoadScene ("result");
				loser = Server_or_Client.role;
			}
		}
			
	}
}
