using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorVisualize : MonoBehaviour {

	int _sensor1;
	int _sensor2;

	// デバイスからのメッセージを分解してintに格納
	void GetAccele(string str){

		string[] receive = str.Split (',');

		_sensor1 = Mathf.FloorToInt (float.Parse(receive [0]));
		_sensor2 = Mathf.FloorToInt (float.Parse(receive [1]));

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (solo_managaer._OnStart == true) {
			
			// 加速度計の値を取得
			GetAccele (BluetoothManager_solo._fromDevice);

			// 横の傾き以外を検知しないように、かつ左右のかたむけを検知できるように
			if (-45 <= _sensor1 && _sensor1 < 45) {
				this.gameObject.transform.rotation = Quaternion.Euler (0, 0, _sensor2);
			} else if (_sensor1 <= -135 || _sensor1 > 135) {
				this.gameObject.transform.rotation = Quaternion.Euler (0, 0, -_sensor2);
			}
		}
		
	}
}
