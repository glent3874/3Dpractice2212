using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通往二樓的門
/// </summary>
public class Door2F: MonoBehaviour, Interactable
{
    #region 欄位
    [SerializeField] Animator 門的動畫 = null;
	[SerializeField] NpcData 沒鑰匙的劇情 = null;
	[SerializeField] NpcData 有鑰匙的劇情 = null;
	[SerializeField] int 鑰匙ID = 0;
	[SerializeField] string saveKey = "2F門";
    #endregion

    #region 事件
    private void Start()
	{
		//門已經被打開? 開門:不動
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
			門的動畫.SetBool("開關", true);
	}
    #endregion

    #region 方法
	/// <summary>
	/// 互動
	/// </summary>
    public void Interact()
	{
		//如果有鑰匙就開門
		if (PlayerInfoManager.instance.是否有(鑰匙ID))
		{
			對話系統.instance.開始對話(有鑰匙的劇情);
			門的動畫.SetBool("開門", true);

			PlayerInfoManager.instance.SetBool(saveKey, true);		//儲存門的狀態
		}
		else
		{
			對話系統.instance.開始對話(沒鑰匙的劇情);
		}
	}
    #endregion
}
