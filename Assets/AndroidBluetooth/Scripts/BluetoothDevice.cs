
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothDevice : MonoBehaviour
{
    //privateにしました。
    private static AndroidJavaObject btDevice;
    //private string selectedDevice;

	public Text tex;

    // Use this for initialization
    void Start()
    {
        init();
    }

    private void init()
    {
        //プラグイン取得、受信時のコールバック関数設定
        btDevice = new AndroidJavaObject("com.example.somen.androidbtspp_split.btspp", this.gameObject.name, "receivedFromDevice", "client");

        ////BluetoothデバイスのリストをCSV形式で取得
        //string devListCsv = btClient.Call<string>("getDeviceList");
        //Debug.Log(devListCsv);

        ////csv分割
        //string[] devList = devListCsv.Split(',');

        ////ここでユーザに選択画面を出す
        //selectedDevice = devList[0];//仮実装。とりあえずリストの先頭のものを選択

        //【NEW!!】UUIDの変更
        btDevice.Call("setBtUuid", "00001101-0000-1000-8000-00805F9B34FB");//UUIDを、デバイスで設定したものと共通のものに変更

        //【EDITED!!】サーバデバイスと接続
        btDevice.Call("runAsClient", "ICE EXCITING!");//サーバのデバイスを指定して接続
    }

    private void Update()
    {
        ////サーバにメッセージ送信
        //string sendMessage = "getAccel";
        //if(btClient!=null)btClient.Call("send", sendMessage);
    }

    //コールバック
    void receivedFromDevice(string message)
    {
		tex.text = message;
        ////csv分割
        //string[] splitted = message.Split(',');
        Debug.Log("received:" + message);
        //Debug.Log("splitted[0]:" + splitted[0]);
    }

    //アプリポーズ時の処理呼び出し
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            btDevice.Call("pause");
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
 * 
 * 【NEW!!】
 * public void  setBtUuid(String uuid)
 * Bluetoothのサービスを識別するUUIDを変更する。
 * サーバとクライアント間で同一のUUIDの場合のみ接続できる。サービス識別という意味では、TCP/IPでいうところのポート番号のようなものか。
 * 
----------------*/
