using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager: MonoBehaviour
{
	Dropdown dd;

	public static string Client_or_Server;

	static AndroidJavaObject btClient;
	string selectedDevice;

	void Start(){
		dd = GameObject.Find ("DeviceList").GetComponent <Dropdown> ();

		Client_or_Server = "";
	}
		
	//<summary>
	// serverかclientか選択してデバイスも選択した後に接続 
	//</summary>
	public void ConnectDevice(string role){

		// ドロップダウンメニューから選択されたデバイスをserverに設定
		selectedDevice = dd.captionText.ToString ();

		// サーバデバイスと接続
		btClient.Call ("runAs" + Client_or_Server, selectedDevice);
	}
}

