using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] Animator door;
    [SerializeField] NpcData 描述文本 = null;

    int 互動次數 = 0;
    #endregion

    #region 事件

    #endregion

    #region 方法
    public void interact()
    {
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
