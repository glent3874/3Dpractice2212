using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 放置光球的燈台
/// </summary>
public class LightTable : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] GameObject handLightOnTable = null;    //桌上的光球
    [SerializeField] GameObject handLightOnHand = null;     //手上的光球
    [SerializeField] NpcData 描述文本 = null;               //燈台文本

    int 互動次數 = 0;
    bool playerHandling;
    #endregion

    #region 方法
    /// <summary>
    /// 互動
    /// </summary>
    public void interact()
    {
        //第一次互動描述
        //第二次以後放置&拿起光球
        if(互動次數 == 0)
        {
            對話系統.instance.開始對話(描述文本, this.transform.position);
        }

        if(互動次數 != 0)
        {
            //讀取光球狀態
            playerHandling = handLightOnHand.activeSelf;

            //轉換狀態
            playerHandling = !playerHandling;

            handLightOnHand.SetActive(playerHandling);
            handLightOnTable.SetActive(!playerHandling);
        }
        互動次數++;
    }
    #endregion
    
}
