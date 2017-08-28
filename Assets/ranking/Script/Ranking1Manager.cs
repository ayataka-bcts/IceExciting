using NCMB;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ranking1Manager : MonoBehaviour
{
    public string rankingClassName = "Ranking1";	//NCMB側のランキングクラス名//
    public int count = 10;							//いくつまでランキングデータを取得するか//

    private List<Ranking1Data> rankingDataList = new List<Ranking1Data>();	//取得したランキングデータ//
    public bool IsRankingDataValid { get; private set; }					//ランキングデータの取得に成功しているか//

    public int PlayerCount { get; private set; }			//いままで何人がスコアを登録したか//

    private string currentObjectid;							//自分のスコアのidを一時保存する//

    public static Ranking1Manager Instance;					//シングルトン//

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            CheckNCMBValid();
        }
    }

	// ランキングからデータを取得する
    public void FetchRanking( UnityAction callback = null)
    {
        if (CheckNCMBValid() == false)
        {
            if (callback != null) callback();
            return;
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(rankingClassName);

        //Scoreの値で降順にソート//
        query.OrderByDescending("Score");

        //取得数の設定//
        query.Limit = count;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                IsRankingDataValid = false;
            }
            else
            {
                int num = 1;

                rankingDataList.Clear();

                foreach (NCMBObject obj in objList)
                {
                    rankingDataList.Add(new Ranking1Data(
                         num++,
                         name: obj["Name"] as string,
                         score: Convert.ToInt32(obj["Score"]),
                         objectid: obj.ObjectId
                        ));
                }
                IsRankingDataValid = true;
            }

            if (callback != null) callback();
        });
    }

	// ランキングに登録する
    public void SaveRanking( string name, int score, UnityAction callback = null)
    {
		//スコアがゼロなら登録しない//
		if (CheckNCMBValid() == false)
		{
			if (callback != null) callback();
			return;
		}

		NCMBObject currentObject = new NCMBObject(rankingClassName);

		//現在のユーザー取得
		NCMBUser currentUser = NCMBUser.CurrentUser;
		name = currentUser.UserName;

		//ランキング内にcurrentUserと同名があるか
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(rankingClassName);
		query.WhereEqualTo("Name", currentUser.UserName);

		NCMBObject ncmbObject = new NCMBObject(rankingClassName);
		query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
			if (e == null)
			{ // データの検索が成功したら、
				if (objList.Count == 0)
				{ // ハイスコアが未登録の場合
					// オブジェクトに値を設定
					ncmbObject["Name"] = name;
					ncmbObject["Score"] = 1;
					ncmbObject.SaveAsync(); // セーブ
				}
				else
				{ // ハイスコアが登録済みの場合
//					int cloudScore = System.Convert.ToInt32(objList[0]["Score"]); // クラウド上のスコアを取得
//					if (score > cloudScore)
//					{ // 今プレイしたスコアの方が高かったら、
						objList[0]["Score"] = score; // それを新しいスコアとしてデータを更新
						objList[0].SaveAsync(); // セーブ
//					}
				}
			}
			else
			{
				//エラー処理
			}
		});
    }

    public List<Ranking1Data> GetRanking1()
    {
        //すでにStart()でフェッチ済みのデータを渡すだけ//
        return rankingDataList;
    }

    public string GetRanking1ByText()
    {
        if (IsRankingDataValid)
        {
            string text = string.Empty;

            foreach (Ranking1Data rankingData in rankingDataList)
            {
                // string gameMode = string.Format("{0, 2}", rankingData.gameMode);
                string rankNum = string.Format("{0, 2}", rankingData.rankNum);
                string name = string.Format("{0, -10}", rankingData.name);
                string score = string.Format("{0, -10}", rankingData.score.ToString());

                //さっき保存したスコアがあった場合は赤に着色する//
                if (rankingData.objectid == currentObjectid)
                {
                    text += "<color=red>" + rankNum + "," + name + "," + score + "</color>";
                }
                else
                {
                    text += rankNum + "," + name + "," + score + ",";
                }
            }

            return text;
        }
        else
        {
            return "No Ranking Data";
        }
    }

    public void FetchPlayerCount(UnityAction callback = null)
    {
        if (CheckNCMBValid() == false)
        {
            if (callback != null) callback();
            return;
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(rankingClassName);
        query.CountAsync((int count, NCMBException e) => {
            if (e != null)
            {
                //接続失敗//
            }
            else
            {
                //接続成功//
                PlayerCount = count;
            }
            if (callback != null) callback();
        });
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

public class Ranking1Data
{
    public readonly int rankNum;//順位（本クラス内でつける）//
    public readonly string name;//プレイヤー名//
    public readonly int score;//点数//
    public readonly string objectid;//NCMBのオブジェクトID//

    public Ranking1Data(int rankNum,  string name, int score, string objectid)
    {
        this.rankNum = rankNum;
        this.name = name;
        this.score = score;
        this.objectid = objectid;
    }
}