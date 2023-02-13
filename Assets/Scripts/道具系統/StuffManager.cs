using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager
{
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

    StuffData[] 所有的道具文本 = new StuffData[0];


    bool isLoad = false;
    public void Load()
    {
        if (isLoad)
            return;
        isLoad = true;
        所有的道具文本 = Resources.LoadAll<StuffData>("");
    }

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
}
