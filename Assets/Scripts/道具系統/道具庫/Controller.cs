using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可互動
/// 開門的遙控器
/// </summary>
public class Controller : MonoBehaviour, Interactable
{
    #region 欄位
    [SerializeField] Animator door = null;                 //開門動畫
    [SerializeField] NpcData 描述文本 = null;       //描述文本
    [SerializeField] Animator 燈台 = null;
    [SerializeField] GameObject 燈台光 = null;

    public int 互動次數 = 0;
	#endregion

	#region 方法
	/// <summary>
	/// 互動
	/// </summary>
	public virtual void Interact()
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
            燈台.SetBool("收掉", true);
            燈台光.SetActive(false);

            Destroy(this.gameObject);
        }
        互動次數++;
    }
    #endregion
}
