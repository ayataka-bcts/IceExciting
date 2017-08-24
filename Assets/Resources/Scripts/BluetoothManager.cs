using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager: MonoBehaviour
{

	static AndroidJavaObject btClient;
	static AndroidJavaObject btServer;
	static AndroidJavaObject btDevice;

	public Text tx;
	public Text tx2;

	bool init_set;
	bool send_set;

	// サーバとして接続
	void init_server()
	{
		//プラグイン取得、受信時のコールバック関数設定
		btServer = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "server");

		btServer.Call ("runAsServer");
	}

	// クライエントとして接続
	void init_client()
	{
		//プラグイン取得、受信時のコールバック関数設定
		btClient = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "client");

		// 接続開始
		btClient.Call ("runAsClient", Server_or_Client.device_name);
	}

	// デバイスと接続
	void init_device(){

		// プラグイン取得、受信時のコールバック関数設定
		btDevice = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "receivedFromDevice", "client");

		//【NEW!!】UUIDの変更
		btDevice.Call("setBtUuid", "00001101-0000-1000-8000-00805F9B34FB");//UUIDを、デバイスで設定したものと共通のものに変更

		//【EDITED!!】サーバデバイスと接続
		btDevice.Call("runAsClient", "BTstack SPP Counter");//サーバのデバイスを指定して接続
	}

	void InitServerClient(string role){
		 
		// roleに応じてそれぞれのセットアップ
		if (role == "server") {
			init_server ();
			init_device ();
			init_set = true;
		} else if (role == "client") {
			init_client ();
			init_device ();
			init_set = true;
		}
	}

	void Start(){
		init_set = false;
		send_set = false;
	}

	void Update(){
		
		// 接続開始
		if (init_set == false) {
			InitServerClient (Server_or_Client.role);
		} 

		// 接続完了後
		if (init_set == true && send_set == false) {
			if (Server_or_Client.role == "client") {
				btClient.Call ("send", "ok_client");
			} else if (Server_or_Client.role == "server") {
				btServer.Call ("send", "ok_server");
			}
		}
	}

	void received(string message){
		tx2.text = "receive:" + message;
		send_set = true;
	}


		
}

