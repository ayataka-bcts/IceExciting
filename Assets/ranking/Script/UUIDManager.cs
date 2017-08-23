using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NCMB;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UUIDManager : MonoBehaviour {

    public string UserData = "UserData";//NCMB側のランキングクラス名//
    public int count = 10;//いくつまでランキングデータを取得するか//
    private List<UserData> UserDataList = new List<UserData>();//取得したランキングデータ//
    public bool IsUserDataValid { get; private set; }//ランキングデータの取得に成功しているか//

    public InputField UserName ;

    private string currentObjectid;//自分のスコアのidを一時保存する//

    public static UUIDManager Instance;//シングルトン//

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static string GetOrCreateUUID()
    {
        string uuid = PlayerPrefs.GetString("uuid", "");
        if (uuid != "")
        {
            Debug.Log(uuid);
            return uuid;
        }
        else
        {
            // Global Unique IDentifier
            System.Guid guid = System.Guid.NewGuid();
            uuid = guid.ToString().Replace("-", "");
            PlayerPrefs.SetString("uuid", uuid);
            PlayerPrefs.Save();
            Debug.Log(uuid);
            return uuid;
        }
    }

    public void FetchUserData( UnityAction callback = null)
    {
        if (CheckNCMBValid() == false)
        {
            if (callback != null) callback();
            return;
        }

        //userdataを取得
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(UserData);

        /*//uuid取得
        string uuid = GetOrCreateUUID();

        //uuidでデータを絞る
        query.WhereEqualTo("UUID", uuid);
        */

        //取得数の設定//
        query.Limit = count;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                IsUserDataValid = false;
            }
            else
            {
                UserDataList.Clear();

                foreach (NCMBObject obj in objList)
                {
                    UserDataList.Add(new UserData(
                         uuid: obj["UUID"]as string,
                         name: obj["Name"] as string,
                         objectid: obj.ObjectId
                        ));
                }
                IsUserDataValid = true;
            }

            if (callback != null) callback();
        });
    }

    public List<UserData> GetUserData()
    {
        //すでにStart()でフェッチ済みのデータを渡すだけ//
        return UserDataList;
    }

    public void SaveUserData(string uuid, string name, UnityAction callback = null)
    {
        //スコアがゼロなら登録しない//
        if (CheckNCMBValid() == false || string.IsNullOrEmpty(name))
        {
            if (callback != null) callback();
            return;
        }

        //rankingClassNameに設定したオブジェクトを作る//
        NCMBObject ncmbObject = new NCMBObject(UserData);

        //nameが空だったらNoNameと入れる//
        if (string.IsNullOrEmpty(name)) name = "No Name";

        // オブジェクトに値を設定
        ncmbObject["UUID"] = uuid;
        ncmbObject["Name"] = name;

        // データストアへの登録
        ncmbObject.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                //接続失敗//
            }
            else
            {
                //接続成功//
                //保存したオブジェクトidを記録//
                currentObjectid = ncmbObject.ObjectId;
            }

            //ランキングの更新//
            if (callback != null)
            {
                FetchUserData( callback);
            }
            else
            {
                FetchUserData();
            }

        });
    }

    public void Registration()
    {
        /*
         端末から取得したUUIDと入力されたユーザー名を、サーバに登録する
         */

         //uuid取得
        string uuid = GetOrCreateUUID();

        //uuidとnameを保存
        if (string.IsNullOrEmpty(UserName.ToString()))//InpuFieldが空でないとき
        {
            string userName = UserName.ToString();
            SaveUserData(uuid, userName);
            //画面遷移
            SceneManager.LoadScene("MenuScene");
        }


    }

    public void SignIn()
    {
        /*
         端末からUUIDを取得し、サーバからユーザ名を取得
         UUIDとユーザー名を保持し、以後サーバに保存するときに使用
         */

        string uuid = GetOrCreateUUID() ;

        FetchUserData();


    }


    private bool CheckNCMBValid()
    {
#if UNITY_WEBGL
            Debug.LogWarning("NCMB SDK はWebGLに対応していません。");
            return false;
#else
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogWarning("ネットワーク接続がありません。");
            return false;
        }
        else
        {
            return true;
        }
#endif
    }
}

public class UserData
{
    public readonly string name;//プレイヤー名//
    public readonly string uuid; //uuid//
    public readonly string objectid;//NCMBのオブジェクトID//

    public UserData(string uuid,  string name,  string objectid)
    {
        this.uuid = uuid;
        this.name = name;
        this.objectid = objectid;
    }
}
