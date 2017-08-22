
﻿using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothClient : MonoBehaviour
{
    static AndroidJavaObject btClient;
    string selectedDevice;

	public Dropdown dd;
	public Text debug_text;
	public Text debug_status;

	public GameObject device_list;
	public GameObject connect_button_client;

    // Use this for initialization
    void Start()
    {
    }

	// Bluetoothデバイスのリストを取得してドロップダウンメニューに表示する
	void ShowDeviceList(){

		//BluetoothデバイスのリストをCSV形式で取得
		string devListCsv = btClient.Call<string>("getDeviceList");

		//csv分割
		string[] devList = devListCsv.Split(',');

		// 取得したデータをDropdownメニューに格納
		foreach(string item in devList){
			Dropdown.OptionData od = new Dropdown.OptionData();
			od.text = item;
			dd.options.Add (od);
		}
	}

    public void init()
    {
		//プラグイン取得、受信時のコールバック関数設定
		btClient = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "client");

		//BluetoothManager.Client_or_Server = "Client";
		debug_status.text = "Client";

		// connectボタン、ドロップダウンメニューの表示
		connect_button_client.SetActive (true);
		device_list.SetActive (true);

		ShowDeviceList ();

		selectedDevice = dd.captionText.text;

		debug_text.text = selectedDevice.ToString ();

		btClient.Call ("runAsClient", selectedDevice);

    }

	// サーバかクライエントかどちらかを指定して接続
	public void Connecting(){

		selectedDevice = dd.captionText.ToString ();

		btClient.Call ("runAsClient", selectedDevice);
	}

    private void Update()
    {
        //サーバにメッセージ送信
        string sendMessage = "client_ok";

        if(btClient!=null)
			btClient.Call("send", sendMessage);
    }

    //コールバック
    void received(string message)
    {
		if(message == "server_ok")
			debug_text.text = "ok";

        //csv分割
        string[] splitted = message.Split(',');
        Debug.Log("received:" + message);
        Debug.Log("splitted[0]:" + splitted[0]);
    }

    //アプリポーズ時の処理呼び出し
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //btClient.Call("pause");
        }
        else
        {
            //init();
        }
    }
}

/*--
 * PlayerSettingsで、Minimum API Levelを4.4 KitKat (API level 19)にしてください。
 --*/

/*------プラグインから呼び出せる関数の解説------
 * 
 * public btspp(String gameObjName, String callBackName, String runAs) 
 * コンストラクタ
 * 引数1　アタッチしたGameObjectの名前。通常はthis.gameObject.name
 * 引数2　メッセージ受信時のコールバック関数名。メッセージ受信時に非同期で呼び出される。
 * 引数3　役割の指定。サーバとして動作するのか、クライアントとして動作するのか（server or client）
 * 
 * 
 * public String getDeviceList()
 * ＜クライアント用＞ペアリングしたことのあるBluetoothデバイスのリストを返す。
 * 引数なし
 * 戻り値　カンマ区切りのデバイス名リスト
 * 
 * 
 * public void runAsClient(String ConnectDevName)
 * ＜クライアント用＞接続先デバイスを指定して、受信スレッドを開始。
 * 引数1　接続先デバイスの名前
 * 
 * 
 * public void runAsServer()
 * ＜サーバ用＞受信スレッドを開始。接続待機。
 * 引数なし
 * 
 * 
 * public void pause()
 * Unityアプリがポーズするときに呼んでください。
 * 
 * 
 * public void resume(){
 * Unityアプリが再開するときに呼んでください。
 * 
 * 
 * public void send(String message)
 * 接続先（サーバorクライアント）にメッセージを送信。接続が確立していないと送れない。
 * 引数1　送信したいメッセージ
 * 
 * 
 * public void setRecieveThreadWaitTimeMillisecond(int waitTime){
 * 受信スレッドのループの間に挟むWait時間をミリセカンド単位で設定する。デフォルトは50ms。
 * 引数1　待機時間（ms）
 * 
----------------*/
//=======
//﻿
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class BluetoothClient : MonoBehaviour
//{
//    //privateにしました。
//    private static AndroidJavaObject btClient;
//    private string selectedDevice;
//
//    private static int counter = 0;
//   
//
//    // Use this for initialization
//    void Start()
//    {
//        init();
//    }
//
//    private void init()
//    {
//        //プラグイン取得、受信時のコールバック関数設定
//        btClient = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "client");
//
//        //if (selectedDevice == null)
//        //{
//            //BluetoothデバイスのリストをCSV形式で取得
//            string devListCsv = btClient.Call<string>("getDeviceList");
//            Debug.Log(devListCsv);
//
//            //csv分割
//            string[] devList = devListCsv.Split(',');
//
//            //ここでユーザに選択画面を出す
//            selectedDevice = devList[0];//仮実装。とりあえずリストの先頭のものを選択
//        //}
//
//        //サーバデバイスと接続
//        btClient.Call("runAsClient", selectedDevice);//サーバのデバイスを指定して接続
//    }
//
//    private void Update()
//    {
//        //サーバにメッセージ送信
//        string sendMessage = "getAccel";
//        if(btClient!=null && ++counter%30==0)btClient.Call("send", sendMessage);
//    }
//
//    //コールバック
//    void received(string message)
//    {
//        //csv分割
//        string[] splitted = message.Split(',');
//        Debug.Log("received:" + message);
//        Debug.Log("splitted[0]:" + splitted[0]);
//    }
//
//    //アプリポーズ時の処理呼び出し
//    void OnApplicationPause(bool pauseStatus)
//    {
//        if (pauseStatus)
//        {
//            btClient.Call("pause");
//        }
//        else
//        {
//            init();
//        }
//    }
//}

/*--
 * PlayerSettingsで、Minimum API Levelを4.4 KitKat (API level 19)にしてください。
 --*/

/*------プラグインから呼び出せる関数の解説------
 * 
 * public btspp(String gameObjName, String callBackName, String runAs) 
 * コンストラクタ
 * 引数1　アタッチしたGameObjectの名前。通常はthis.gameObject.name
 * 引数2　メッセージ受信時のコールバック関数名。メッセージ受信時に非同期で呼び出される。
 * 引数3　役割の指定。サーバとして動作するのか、クライアントとして動作するのか（server or client）
 * 
 * 
 * public String getDeviceList()
 * ＜クライアント用＞ペアリングしたことのあるBluetoothデバイスのリストを返す。
 * 引数なし
 * 戻り値　カンマ区切りのデバイス名リスト
 * 
 * 
 * public void runAsClient(String ConnectDevName)
 * ＜クライアント用＞接続先デバイスを指定して、受信スレッドを開始。
 * 引数1　接続先デバイスの名前
 * 
 * 
 * public void runAsServer()
 * ＜サーバ用＞受信スレッドを開始。接続待機。
 * 引数なし
 * 
 * 
 * public void pause()
 * Unityアプリがポーズするときに呼んでください。
 * 
 * 
 * public void resume(){
 * Unityアプリが再開するときに呼んでください。
 * 
 * 
 * public void send(String message)
 * 接続先（サーバorクライアント）にメッセージを送信。接続が確立していないと送れない。
 * 引数1　送信したいメッセージ
 * 
 * 
 * public void setRecieveThreadWaitTimeMillisecond(int waitTime){
 * 受信スレッドのループの間に挟むWait時間をミリセカンド単位で設定する。デフォルトは50ms。
 * 引数1　待機時間（ms）
 * 
----------------*/
