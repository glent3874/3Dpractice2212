using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 切換場景的淡入淡出
/// </summary>
public class 轉場器 : SingletonMonoBehaviour<轉場器>
{
    #region 欄位
    [SerializeField] Animator 動畫 = null;

	string 要切的關卡名稱 = "";
	System.Action 畫面變黑之後要做的事情;
	#endregion

	#region 方法
	/// <summary>
	/// 使用委派執行轉場
	/// </summary>
	/// <param name="_畫面變黑之後要做的事情"></param>
	public void 轉場(System.Action _畫面變黑之後要做的事情)
	{
		this.畫面變黑之後要做的事情 = _畫面變黑之後要做的事情;

		動畫.SetTrigger("離場");
		Invoke("變黑做事", 1.05f);
	}

	/// <summary>
	/// 等待動畫時間後執行委派事件
	/// </summary>
	void 變黑做事()
	{
		if (畫面變黑之後要做的事情 != null)
			畫面變黑之後要做的事情.Invoke();
	}

	/// <summary>
	/// 依據傳入的關卡名稱直接轉場
	/// </summary>
	/// <param name="關卡名稱"></param>
	public void 轉場(string 關卡名稱)
	{
		要切的關卡名稱 = 關卡名稱;

		動畫.SetTrigger("離場");
		Invoke("切換關卡", 1.05f);
	}

	void 切換關卡()
	{
		SceneManager.LoadScene(要切的關卡名稱);
	}
	#endregion
}
