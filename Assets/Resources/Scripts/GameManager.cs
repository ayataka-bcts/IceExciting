using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// 加速度センサの値(仮想)
	public float axis_x;
	public float axis_y;
	public float axis_z;

	// 今どちらの状態かを判断する変数
	string atack_or_defence;

	// デバイスとBluetoothで接続する
	void ConnectDevice(){
		
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
			}
		} else if (value < 0) {
			if (-90 < value && value < -75) {
				value = -90;
			} else if (-75 < value && value < -60) {
				value = -75;
			} else if (-60 < value && value < -45) {
				value = -60;
			} else if (-45 < value && value < -30) {
				value = -45;
			} else if (-30 < value && value < -15) {
				value = -30;
			} else if (-15 < value && value < 0) {
				value = 0;
			}
		}

		return value;

	}

	// ランダムで指定する角度を生成する
	int GenerateTargetAngle(){

		// -90度から90度までの間で乱数発生
		// [Todo]綺麗な数値の乱数を作れる関数あるかも
		int rnd = Random.Range (-90, 90);

		// 適度な数値にマッピングし直す
		int angle = RangeValueMapping (rnd);

		return angle;
	}

	// 角度の判定(引数：センサ値、指定の角度)
	bool CheckAngle(int sensor, int target){

		// [要調整]いくつかの誤差を許容してもいいかも
		if (sensor == target) {
			return true;
		} else {
			return false;
		}
	}

	// 受信したメッセージを整える
	void ReceiveMessage(string message){
		//おそらくカンマ区切りのstringで送られてくるので
		message.Split (',');
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// アイスを食べる側のとき
		if (atack_or_defence == "atack") {

			// 攻守交代
			atack_or_defence = "defence";
		}
		// アイスを傾ける側のとき
		else if (atack_or_defence == "defence") {
			
			// 指定の角度を生成
			int angle = GenerateTargetAngle ();

			// 加速度計の値を取得(未実装)

			// 値の判定
			if(CheckAngle (int.Parse(axis_x.ToString()), angle)){

			}

			// 攻守交代
			atack_or_defence = "atack";
		}
	}
}
