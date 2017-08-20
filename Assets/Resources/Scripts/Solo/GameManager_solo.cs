using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_solo : MonoBehaviour {

	// デバイスから送信されてくる加速度センサの値
	public string axis_x;
	public string axis_y;
	public string axis_z;

	// デバイスから送信されてくるジャイロセンサの値


	// シーン内のセンサ値を反映するobject
	GameObject _sensor;

	// 変数の初期化
	void SetValue(){
		axis_x = "";	axis_y = "";	axis_z = "";
		_sensor = GameObject.Find ("AxisSensor/Cube");
	}

	// string(おそらく来るデータ)をfloatなりなんなりに
	float ConvertFloot(string str){
		float value;
		value = float.Parse (str);

		return value;
	}

	// センサの値に応じて物体が動く
	void ConnectionSensor(){

		float value_x = ConvertFloot (axis_x);
		float value_y = ConvertFloot (axis_y);
		float value_z = ConvertFloot (axis_z);

		// センサの値に応じて物体を傾ける
		_sensor.transform.rotation = Quaternion.Euler (value_x, value_y, value_z);
	}

	// Use this for initialization
	void Start () {
		SetValue ();

		Dropdown dp = GameObject.Find ("DeviceList").GetComponent <Dropdown> ();
		Dropdown.OptionData od = new Dropdown.OptionData();
		od.text = "test";
		Debug.Log ("1:" + dp);

		dp.options[1] = od;

		Debug.Log ("2:" + dp);

	}
	
	// Update is called once per frame
	void Update () {
		ConnectionSensor ();
	}
}
