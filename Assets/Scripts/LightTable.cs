using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTable : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] GameObject handLightOnTable = null;
    [SerializeField] GameObject handLightOnHand = null;
    [SerializeField] NpcData 描述文本 = null;

    int 互動次數 = 0;
    bool playerHandling;
    #endregion

    #region 事件

    #endregion

    #region 方法
    public void interact()
    {
        if(互動次數 == 0)
        {
            對話系統.instance.開始對話(描述文本, this.transform.position);
        }

        if(互動次數 != 0)
        {
            //讀取光球手持狀態
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
