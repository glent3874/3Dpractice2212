using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 開門時鏡頭的轉換
/// </summary>
public class Door : MonoBehaviour
{
    #region 欄位
    [SerializeField] GameObject cinamachine = null;     //攝影機
    #endregion

    #region 方法
    /// <summary>
    /// 開始鏡頭轉換
    /// </summary>
    public void StartAnimation()
    {
        cinamachine.SetActive(true); 
    }

    /// <summary>
    /// 結束鏡頭轉換
    /// </summary>
    public void EndAnimation()
    {
        Destroy(cinamachine);
        Destroy(this.gameObject);
    }
    #endregion
}
