using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1F 的槍
/// </summary>
public class Pistol : Controller
{
    #region 欄位
    [SerializeField] public GameObject mainCam;
    #endregion

    #region 方法
    public override void Interact()
    {
        base.Interact();

        if (互動次數 > 1)
        {
            mainCam.GetComponent<Shoot>().havePistol = true;

            PlayerInfoManager.instance.AddItem(1);
        }
    }
    #endregion
}
