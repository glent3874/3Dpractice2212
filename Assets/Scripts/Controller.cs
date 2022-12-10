using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, Item
{
    #region 欄位
    [SerializeField] Animator door;
    #endregion

    #region 事件
    #endregion

    #region 方法
    public void interact()
    {
        door.SetBool("開門", true);
        Destroy(this.gameObject);
    }
    #endregion
}
