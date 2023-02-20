using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具資料的管理系統
/// 圖書館員
/// </summary>
public class StuffManager
{
    #region 單例模式
    public static StuffManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new StuffManager();
            }
            return _instance;
        }
    }
    static StuffManager _instance = null;
    #endregion

    #region 欄位
    StuffData[] 所有的道具文本 = new StuffData[0];

    bool isLoad = false;
    #endregion

    #region 方法
    /// <summary>
    /// 讀取道具資料
    /// </summary>
    public void Load()
    {
        if (isLoad) return;
        isLoad = true;
        //將 Resources 資料夾中的所有 StuffData 檔讀進來
        //記到查詢系統中
        所有的道具文本 = Resources.LoadAll<StuffData>("");
    }

    /// <summary>
    /// 從資料中取得編號
    /// </summary>
    /// <param name="id">要查的道具id</param>
    /// <returns>回傳道具的詳細資料</returns>
    public StuffData GetDataById(int id)
    {
        for (int i = 0; i < 所有的道具文本.Length; i++) 
        {
            if(所有的道具文本[i].道具編號 == id)
            {
                return 所有的道具文本[i];
            }
        }
        return null;
    }
    #endregion
}
