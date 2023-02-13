using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    #region 方法
    public void AddItem(int 道具編號)
    {
        bool 添加成功 = false;
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
        {
            if(SaveManager.instance.saveData.stuffs[i].id == 道具編號)
            {
                Stuff temp = SaveManager.instance.saveData.stuffs[i];
                temp.count++;
                SaveManager.instance.saveData.stuffs[i] = temp;
                添加成功 = true;
                break;
            }
        }

        if (添加成功 == false)
        {
            Stuff temp = new Stuff();
            temp.id = 道具編號;
            temp.count = 1;
            SaveManager.instance.saveData.stuffs.Add(temp);
        }

        if(道具發生變化 != null)
        {
            道具發生變化.Invoke();
        }
    }
    public void RemoveItem(int 道具編號)
    {
        if (道具發生變化 != null)
        {
            道具發生變化.Invoke();
        }
    }
    public Action 道具發生變化 = null;

    public bool 是否有(int 道具編號)
    {
        return false;
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
