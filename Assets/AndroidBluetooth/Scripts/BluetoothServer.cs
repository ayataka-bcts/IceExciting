using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothServer : MonoBehaviour
{

	static AndroidJavaObject btServer;
	string selectedDevice;

	Dropdown dd;
	BluetoothManager bm;
	public Text debug_text;

	// Use this for initialization
	void Start()
	{
		dd = GameObject.Find ("DeviceList").GetComponent <Dropdown> ();
	}

	// Bluetoothデバイスのリストを取得してドロップダウンメニューに表示する
	void ShowDeviceList(){

		//BluetoothデバイスのリストをCSV形式で取得
		string devListCsv = btServer.Call<string>("getDeviceList");
		Debug.Log (devListCsv);

		//csv分割
		string[] devList = devListCsv.Split(',');

		// 取得したデータをDropdownメニューに格納
		foreach(string item in devList){
			Dropdown.OptionData od = new Dropdown.OptionData();
			od.text = item;
			dd.options.Add (od);
		}
	}

	private void init()
	{
		//プラグイン取得、受信時のコールバック関数設定
		btServer = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "received", "server");

		ShowDeviceList ();

		BluetoothManager.Client_or_Server = "Server";
	}


    //コールバック
    void received(string message)
    {
        Debug.Log("received:" + message);

        //受信メッセージが"getAccel"なら加速度を返す（仮実装なのでなんとでも変えてください）
        if (message == "getAccel")
        {
            string sendMessage = "120,113,134";
            btServer.Call("send", sendMessage);
        }
    }

    //アプリポーズ時の処理呼び出し
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            btServer.Call("pause");
        }
        else
        {
            init();
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
