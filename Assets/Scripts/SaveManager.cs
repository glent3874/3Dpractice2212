using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存檔資料管理器
/// </summary>
public class SaveManager
{
    #region 單例模式
    public static SaveManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }
    static SaveManager _instance = null;
    #endregion

    #region 欄位
    public PlayerData saveData;
    public bool dataExist = false;
    public bool continueGame = false;
    #endregion

    #region 方法
    public void 清除資料()
	{
        PlayerPrefs.DeleteKey("PLAYER_DATA");
        Download();
	}

    /// <summary>
    /// 下載檔案
    /// </summary>
    public void Download()
    {
        //從硬碟讀取文件 如果沒有資料就回傳"N"
        string json = PlayerPrefs.GetString("PLAYER_DATA", "N");

        //如果有資料就解析
        if (json != "N") 
        {
            //把json文字轉回玩家資料
            saveData = JsonUtility.FromJson<PlayerData>(json);
            dataExist = true;
        }
        else
        {
            //沒資料
            //分配記憶體給存檔資料
            saveData = new PlayerData();
            saveData.stuffs = new List<Stuff>();
            saveData.sceneDatas = new List<SceneData>();
<<<<<<< HEAD
            dataExist = false;
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
        }
    }

    /// <summary>
    /// 上傳檔案
    /// </summary>
    public void Upload()
    {
        //把玩家資料轉成json文字
        string json = JsonUtility.ToJson(saveData, true);
        Debug.Log(json);
        //把文件丟到硬碟中 命名為PLAYER_DATA
        PlayerPrefs.SetString("PLAYER_DATA", json);
    }
    #endregion
}

#region 結構
/// <summary>
/// 玩家存檔資料
/// </summary>
[System.Serializable]
public struct PlayerData
{
    public string levelName;            //當前關卡
    public Vector3 playerPos;           //玩家位置
    public Vector3 playerRotateY;       //玩家面向
    public List<Stuff> stuffs;          //玩家身上的道具資料
    public List<SceneData> sceneDatas;
}

<<<<<<< HEAD
/// <summary>
/// 場景資料
/// </summary>
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
[System.Serializable]
public struct SceneData
{
    public string key;
    public bool flag;
    public Vector3 pos;
    public string info;
}
#endregion