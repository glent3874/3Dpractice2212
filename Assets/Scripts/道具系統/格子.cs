using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 格子 : MonoBehaviour
{
    [SerializeField] Image 圖示 = null;
    [SerializeField] Text 標題 = null;

    //暫存道具資料
    StuffData stuffData;

    /// <summary>
    /// AddItem
    /// </summary>
    /// <param name="theStuffPlayerOwn">Id, 數量</param>
    public void 接收資料(Stuff theStuffPlayerOwn)
    {
        stuffData = StuffManager.instance.GetDataById(theStuffPlayerOwn.id);

        圖示.enabled = true;
        圖示.sprite = stuffData.道具圖片;
        if(theStuffPlayerOwn.count >= 2)
        {
            標題.enabled = true;
            標題.text = theStuffPlayerOwn.count + "";
        }
    }

    public void 清除資料()
    {
        stuffData = null;

        圖示.sprite = null;
        圖示.enabled = false;
        標題.text = null;
        標題.enabled = false;
    }
}
