using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager: MonoBehaviour
{
	// Bluetooth接続関連
	static AndroidJavaObject btClient;
	static AndroidJavaObject btServer;
	static AndroidJavaObject btDevice;

	// デバッグ用テキスト
	public Text tx;
	public Text tx2;
	public Text tx3;

	// デバイスから送られてきた文字列
	public static string _fromDevice;

	// セットアップ関連のトリガー
	bool init_set;
	bool send_set;

	// ゲーム終了時の合図
	public static bool end_ok;

	public static bool change_ok;

	public GameObject cutin;

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
		btDevice.Call("runAsClient", "ICE EXCITING!");//サーバのデバイスを指定して接続
	}

	// 端末間同士とデバイス接続のセットアップ
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
		end_ok = false;
		change_ok = false;
	}

	void Update(){
		
		// 接続開始
		if (init_set == false) {
			InitServerClient (Server_or_Client.role);
		} 

		// 接続完了後(相互にメッセージを送る)
		if (init_set == true && send_set == false) {
			if (Server_or_Client.role == "client") {
				btClient.Call ("send", "ok_client");
			} else if (Server_or_Client.role == "server") {
				btServer.Call ("send", "ok_server");
			}
		}

		// 攻守交代になれば合図を送る
		if (GameManager.change_trigger == true) {
			if (GameManager.atack_or_defence == "defence") {
				SendChangeSign ();
				GameManager.atack_or_defence = "atack";
			} else if (GameManager.atack_or_defence == "atack") {
				GameManager.atack_or_defence = "defence";
				GameManager.end_atack = false;
				GameManager.change_trigger = false;
				btDevice.Call ("send", "ICE");
			}
		}

		// ゲームが終了した時(敗者用の処理)
		if (GameManager.end_game == true) {
			if (Server_or_Client.role == "client") {
				btClient.Call ("send", "end");
			} else if (Server_or_Client.role == "server") {
				btServer.Call ("send", "end");
			}
		}
	}

	// 攻守交代の合図を送信する
	void SendChangeSign(){
		
		if (Server_or_Client.role == "client") {
			btClient.Call ("send", "change");
			GameManager.change_trigger = false;
		} else if (Server_or_Client.role == "server") {
			btServer.Call ("send", "change");
			GameManager.change_trigger = false;
		}

		btDevice.Call ("send", "ICE");
	}

	// 端末間同士通信でのメッセージ受信時
	void received(string message){

		GameObject.Find ("debug").GetComponent <Text> ().text = "rec:" + message; 

		// 接続完了したかどうか「ok」を含むかどうかを判定
		if (message.Contains ("ok")) {
			send_set = true;
		}

		// 攻守交代の合図を受け取ったら(攻撃側が受け取る)
		if (message.Contains ("change")) {
			
			// 攻守の状態を確認して別の方に変更
			if (GameManager.atack_or_defence == "atack") {
				//StartCoroutine (ChangeTurn ());
				GameManager.end_atack = true;
				//btDevice.Call ("send", "ICE");
			} 
		}

		// 終了の合図を受け取ったら
		if (message.Contains ("end")) {
			end_ok = true;
		}
	}

	// デバイスからのメッセージ受信
	void receivedFromDevice(string message){

		// 受信したメッセージをGameManagerに渡す
		_fromDevice = message;
	}

		
}

