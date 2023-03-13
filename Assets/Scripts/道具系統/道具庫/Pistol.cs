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
    [SerializeField] string saveKey = "1F傳送門";
	#endregion

	#region 事件
	private void Start()
	{
        if (PlayerInfoManager.instance.GetBool(saveKey) == true)
		{
            mainCam.GetComponent<Shoot>().havePistol = true;
            Destroy(this.gameObject);
		}
    }
	#endregion

	#region 方法
	public override void Interact()
    {
        base.Interact();

        if (互動次數 > 1)
        {
            mainCam.GetComponent<Shoot>().havePistol = true;

            PlayerInfoManager.instance.AddItem(1);

            PlayerInfoManager.instance.SetBool(saveKey, true);
        }
    }
    #endregion
}
