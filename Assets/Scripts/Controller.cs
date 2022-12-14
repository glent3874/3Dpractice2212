using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可互動
/// 開門的遙控器
/// </summary>
public class Controller : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] Animator door;                 //開門動畫
    [SerializeField] NpcData 描述文本 = null;       //描述文本

    int 互動次數 = 0;
    #endregion

    #region 方法
    /// <summary>
    /// 互動
    /// </summary>
    public void interact()
    {
        //第一次互動描述物件
        //第二次開門並移除
        if (互動次數 == 0)
        {
            對話系統.instance.開始對話(描述文本, this.transform.position);
        }
        if (互動次數 == 1)
        { 
            door.SetBool("開門", true);
            Destroy(this.gameObject);
        }
        互動次數++;
    }
    #endregion
}
