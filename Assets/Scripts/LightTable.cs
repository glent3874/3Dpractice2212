using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTable : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] GameObject handLightOnTable = null;
    [SerializeField] GameObject handLightOnHand = null;

    bool playerHandling;
    #endregion

    #region 事件

    #endregion

    #region 方法
    public void interact()
    {
        //讀取光球手持狀態
        playerHandling = handLightOnHand.activeSelf;

        //轉換狀態
        playerHandling = !playerHandling;

        handLightOnHand.SetActive(playerHandling);
        handLightOnTable.SetActive(!playerHandling);
    }
    #endregion
    
}
