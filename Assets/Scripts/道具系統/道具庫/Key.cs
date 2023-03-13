using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鑰匙
/// </summary>
public class Key : MonoBehaviour, Interactable
{
    #region 欄位
    [SerializeField] NpcData 文本 = null;
    [SerializeField] string saveKey = "二樓鑰匙";

    int 互動次數 = 0;
	#endregion

<<<<<<< HEAD
	#region 事件
	private void Start()
	{
        //向玩家資料管理器查詢鑰匙是不是被撿了
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
            Destroy(this.gameObject);
	}
	#endregion
=======
	private void Start()
	{
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
		{
            Destroy(this.gameObject);
		}
	}
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c

	#region 方法
	/// <summary>
	/// 互動
	/// </summary>
	public void Interact()
    {
        if(互動次數 == 0)
        {
            對話系統.instance.開始對話(文本, this.transform.position);
        }
        if(互動次數 == 1)
        {
            PlayerInfoManager.instance.AddItem(0);

            PlayerInfoManager.instance.SetBool(saveKey, true);

            Destroy(this.gameObject);
        }
        互動次數++;
    }
    #endregion
}
