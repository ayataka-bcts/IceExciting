using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager: MonoBehaviour
{

	static AndroidJavaObject btClient;
	static AndroidJavaObject btServer;

	Server_or_Client _soc;

	public Text tx;
	public Text tx2;

	bool init_set;
	bool send_set;

	void init_server()
	{
		//プラグイン取得、受信時のコールバック関数設定
		btServer = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "server");

		btServer.Call ("runAsServer");
	}

	void init_client()
	{
		//プラグイン取得、受信時のコールバック関数設定
		btClient = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "client");

		// 接続開始
		btClient.Call ("runAsClient", Server_or_Client.device_name);
	}

	void InitServerClient(string role){
		if (role == "server") {
			init_server ();
			tx.text = "server is";
			init_set = true;
		} else if (role == "client") {
			init_client ();
			tx.text = "client is";
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

