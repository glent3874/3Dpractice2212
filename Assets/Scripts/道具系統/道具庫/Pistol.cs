using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1F ���j
/// </summary>
public class Pistol : Controller
{
    #region ���
    [SerializeField] public GameObject mainCam;
    #endregion

    #region ��k
    public override void Interact()
    {
        base.Interact();

        if (���ʦ��� > 1)
        {
            mainCam.GetComponent<Shoot>().havePistol = true;

            PlayerInfoManager.instance.AddItem(1);
        }
    }
    #endregion
}
