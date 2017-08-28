using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour {

    public InputField userName;
    public InputField passWord;
    public Text errorText;
    public static Ranking1Manager Instance;//シングルトン//

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//名前をリストに登録する
	public void NameListSignUp(string name)
	{
		//NCMBUserのインスタンス作成
		NCMBUser user = new NCMBUser();

		//ユーザ名とパスワードの設定
		user.UserName = name;
		user.Password = "a";
		//user.Email = "email@example.com";


		//会員登録を行う
		user.SignUpAsync((NCMBException e) => {
			if (e != null)
			{
				UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);

				errorText.GetComponent<Text>().text = e.ErrorMessage;
			}
			else
			{
				UnityEngine.Debug.Log("新規登録に成功");
				SceneManager.LoadScene("MenuScene");
			}
		});
	}

	//受け取った名前でログインする　パスワードは”a”固定
	public static void NameListLogIn(string name)
	{
		// ユーザー名とパスワードでログイン
		NCMBUser.LogInAsync(name, "a", (NCMBException e) => {
			if (e != null)
			{
				//UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
				//errorText.GetComponent<Text>().text = e.ErrorMessage;
			}
			else
			{
				//UnityEngine.Debug.Log("ログインに成功！");
				//SceneManager.LoadScene("MenuScene");
			}
		});
	}

    public void SignUpUser()
    {
        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        //ユーザ名とパスワードの設定
        user.UserName = userName.text.ToString();
        user.Password = passWord.text.ToString();
        //user.Email = "email@example.com";

        // 任意フィールドに値を追加 
        //user.Add("phone", "987-654-3210");

        //会員登録を行う
        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);

                errorText.GetComponent<Text>().text =  e.ErrorMessage;
            }
            else
            {
                UnityEngine.Debug.Log("新規登録に成功");
                SceneManager.LoadScene("MenuScene");
            }
        });

    }

    public void LogIn()
    {
        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(userName.text.ToString(), passWord.text.ToString(), (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
                errorText.GetComponent<Text>().text = e.ErrorMessage;
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
                SceneManager.LoadScene("MenuScene");
            }
        });

    }

    public void LogOut()
    {
        // ログアウト
        try
        {
            NCMBUser.LogOutAsync();
        }
        catch (NCMBException e)
        {
            UnityEngine.Debug.Log("エラー: " + e.ErrorMessage);
            errorText.GetComponent<Text>().text = e.ErrorMessage;
        }

    }

    public string GetLogInUser()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            // ログイン中のユーザーの取得に成功
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
            return currentUser.UserName;
        }
        else
        {
            // 未ログインまたは取得に失敗
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
            
        }
        return "error" ;
    }

    public void moveSignUp()
    {
        SceneManager.LoadScene("NewPlayer");
    }
}


