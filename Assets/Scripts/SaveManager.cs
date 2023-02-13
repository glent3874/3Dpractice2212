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
    /// <summary>
    /// 下載檔案
    /// </summary>
    public void Download()
    {
        //從硬碟讀取文件
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
            //分配記憶體給存檔資料
            saveData = new PlayerData();
            dataExist = false;
            saveData.stuffs = new List<Stuff>();
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
        //把文件丟到硬碟中
        PlayerPrefs.SetString("PLAYER_DATA", json);
    }
    #endregion
}

#region 結構
[System.Serializable]
public struct PlayerData
{
    public string levelName;
    public Vector3 playerPos;
    public Vector3 playerRotateY;
    public List<Stuff> stuffs;
}
#endregion