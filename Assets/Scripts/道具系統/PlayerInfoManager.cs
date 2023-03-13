using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 存取玩家身上的道具資訊(只知道存了什麼, 有幾個)
/// 單例
/// </summary>
public class PlayerInfoManager
{
    #region 單例模式
    public static PlayerInfoManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PlayerInfoManager();
            }
            return _instance;
        }
    }
    static PlayerInfoManager _instance = null;
    #endregion

    #region 欄位
    public Action 道具發生變化 = null;
    #endregion

    #region 方法
    /// <summary>
    /// 獲得道具
    /// </summary>
    /// <param name="道具編號"></param>
    public void AddItem(int 道具編號)
    {
        bool 添加成功 = false;
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
        {
            // 搜尋資料庫中有沒有這個道具
            if(SaveManager.instance.saveData.stuffs[i].id == 道具編號)
            {
                //不能直接對資料修改, 所以取出後修改再存回去
                Stuff temp = SaveManager.instance.saveData.stuffs[i];
                temp.count++;
                SaveManager.instance.saveData.stuffs[i] = temp;
                添加成功 = true;
                break;
            }
        }

        //獲得沒有獲得過的道具就建立新的道具資料存檔
        if (添加成功 == false)
        {
            Stuff temp = new Stuff();
            temp.id = 道具編號;
            temp.count = 1;
            SaveManager.instance.saveData.stuffs.Add(temp);
        }

        // 道具欄 Inventary 登記的委派: 更新UI
        // 在成功存資料後做執行
        if(道具發生變化 != null)
        {
            道具發生變化.Invoke();
        }
    }

    /// <summary>
    /// 使用 或 移除道具
    /// </summary>
    /// <param name="道具編號"></param>
    public void RemoveItem(int 道具編號)
    {
        // 道具欄 Inventary 登記的委派: 更新UI
        // 在成功存資料後做執行
        if (道具發生變化 != null)
        {
            道具發生變化.Invoke();
        }
    }
    
    /// <summary>
    /// 搜尋玩家身上是否有此項道具
    /// </summary>
    /// <param name="道具編號"></param>
    /// <returns></returns>
    public bool 是否有(int 道具編號)
    {
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
		{
            if (SaveManager.instance.saveData.stuffs[i].id == 道具編號)
			{
                return true;
            }
		}
        return false;
    }

<<<<<<< HEAD
    /// <summary>
    /// 檢查場景中的資料
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
    public bool GetBool(string key)
	{
        for (int i = 0; i < SaveManager.instance.saveData.sceneDatas.Count; i++)
		{
            if (SaveManager.instance.saveData.sceneDatas[i].key == key)
			{
                return SaveManager.instance.saveData.sceneDatas[i].flag;
			}
		}
        return false;
	}

<<<<<<< HEAD
    /// <summary>
    /// 存場景中的資料
    /// </summary>
    /// <param name="key"></param>
    /// <param name="v"></param>
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
    public void SetBool(string key, bool v)
	{
        SceneData temp = new SceneData();
        temp.key = key;
        temp.flag = v;
        temp.pos = Vector3.zero;
        temp.info = "";

        for (int i = 0; i < SaveManager.instance.saveData.sceneDatas.Count; i++)
		{
<<<<<<< HEAD
            //如果已經有這筆資料就覆蓋過去
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
            if (SaveManager.instance.saveData.sceneDatas[i].key == key)
			{
                SaveManager.instance.saveData.sceneDatas[i] = temp;
                return;
			}
		}

<<<<<<< HEAD
        SaveManager.instance.saveData.sceneDatas.Add(temp);     //沒有資料就建一個
=======
        SaveManager.instance.saveData.sceneDatas.Add(temp);
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
	}
    #endregion
}

#region 結構
[System.Serializable]
public struct Stuff
{
    public int id;
    public int count;
}
#endregion
