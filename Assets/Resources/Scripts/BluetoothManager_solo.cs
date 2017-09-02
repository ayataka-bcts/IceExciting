using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager_solo: MonoBehaviour
{
	// Bluetooth接続関連
	static AndroidJavaObject btDevice;

	// デバイスから送られてきた文字列
	public static string _fromDevice;

	// 接続が完了したかどうか
	bool init_set;

	string status;

	// デバイスと接続
	void init_device(){

		// プラグイン取得、受信時のコールバック関数設定
		btDevice = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "receivedFromDevice", "client");

		//【NEW!!】UUIDの変更
		btDevice.Call("setBtUuid", "00001101-0000-1000-8000-00805F9B34FB");//UUIDを、デバイスで設定したものと共通のものに変更

		//【EDITED!!】サーバデバイスと接続
		btDevice.Call("runAsClient", "EXCITING ORANGE");//サーバのデバイスを指定して接続
		//btDevice.Call("runAsClient", "ICE BLUE");//サーバのデバイスを指定して接続
	}

	void Start(){
		init_set = false;
		_fromDevice = null;
	}

	void Update(){
		 
		// デバイスとの接続
		if (init_set == false) {
			init_device ();
			if (_fromDevice != null) {
				init_set = true;
			}
		}
	}



	// 攻守交代の合図を送信する
	void SendChangeSign(){

		btDevice.Call ("send", "ICE");
	}

	// デバイスからのメッセージ受信
	void receivedFromDevice(string message){

		// 受信したメッセージをGameManagerに渡す
		_fromDevice = message;
	}


}

