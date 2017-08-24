using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

	// 傾ける時間に応じて食べる時間を算出する際の閾値
	// ゲームバランスに影響するのでとりあえず暫定的に
	public const int fast = 10;
	public const int slow = 20;

	// 加速度センサの値(仮想)
	public float axis_x;
	public float axis_y;
	public float axis_z;

	// 傾けタイムの時間測定用
	public float perform_time;
	// 食べる時間
	public float tasting_time;

	// 食べる時か傾ける時か
	private bool _IsPerform = false;
	private bool _IsTasting = false;

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

	// デバッグ用変数の初期化
	void DebugSetUp(){
	}

	// かかった時間に応じたテイスティングタイムの算出
	int PerformForTasting(int time){

		// 食べられる時間
		int reward = 0;

		if (time < fast) {
			reward = 7;
		} else if (fast <= time && time < slow) {
			reward = 5;
		} else if (slow <= time) {
			reward = 3;
		} 

		return reward;
	}

	// Use this for initialization
	void Start () {
		DebugSetUp ();
	}
	
	// Update is called once per frame
	void Update () {

		// 傾ける時の処理
		if (_IsPerform == true && _IsTasting == false) {
			
			// 指定の角度を生成
			int angle = GenerateTargetAngle ();

			// 加速度計の値を取得(未実装)

			// 計測開始

			// 値の判定
			if(CheckAngle (int.Parse(axis_x.ToString()), angle)){

				// 食べる時間の計測ストップ

				// テイスティングタイムのセット
				tasting_time = 0;

				// 食べる時間に遷移
				_IsPerform = false;
				_IsPerform = true;
			}
		}
		// 食べる時の処理
		else if (_IsPerform == false && _IsTasting == true) {

			// 時間の計測開始

			if(tasting_time == 0){
				// 時間終了時に傾ける時間に遷移
				_IsPerform = true;
				_IsTasting = false;
			}
		}
	}
}
