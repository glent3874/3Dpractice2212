using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 顯示道具的格子
/// </summary>
public class 格子 : MonoBehaviour
{
    #region 欄位
    [SerializeField] Image 圖示 = null;
    [SerializeField] Text 道具數量 = null;

    StuffData stuffData;                                        //暫存道具資料
    #endregion

    /// <summary>
    /// 接收資料並顯示道具
    /// </summary>
    /// <param name="theStuffPlayerOwn">Id, 數量</param>
    public void 接收資料(Stuff theStuffPlayerOwn)
    {
        stuffData = StuffManager.instance.GetDataById(theStuffPlayerOwn.id);    //從圖書館查詢資料並取出

        圖示.enabled = true;                                    //開啟格子上的圖示
        圖示.sprite = stuffData.道具圖片;                        //更改圖片

        if(theStuffPlayerOwn.count >= 2)                        //如果有複數以上的道具就顯示數量
        {
            道具數量.enabled = true;
            道具數量.text = theStuffPlayerOwn.count + "";
        }
    }

    /// <summary>
    /// 道具存檔中沒有更多資料了 清除格子
    /// </summary>
    public void 清除資料()
    {
        stuffData = null;

        圖示.sprite = null;
        圖示.enabled = false;
        道具數量.text = null;
        道具數量.enabled = false;
    }
}
