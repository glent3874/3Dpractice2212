using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品欄裡道具詳細資料的顯示
/// </summary>
public class 詳細資料 : Windows<詳細資料>
{
	#region 欄位
	[SerializeField] Image 圖示 = null;
    [SerializeField] Text 道具名稱 = null;
    [SerializeField] Text 道具敘述 = null;
    [SerializeField] CanvasGroup 使用按鈕透明度 = null;
    [SerializeField] CanvasGroup 刪除按鈕透明度 = null;

    bool 是否維持顯示 = false;
	#endregion

	#region 事件
	protected override void Update()
	{
		base.Update();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen)
			{
                是否維持顯示 = false;
                Close();
			}
        }
	}
	#endregion

	#region 方法
    /// <summary>
    /// 顯示詳細資料
    /// </summary>
    /// <param name="道具資料">要顯示的道具資料</param>
	public void 顯示(Stuff 道具資料)
    {
        StuffData 詳細資料 = StuffManager.instance.GetDataById(道具資料.id);        //向資料庫輸入道具ID搜尋詳細資料

        圖示.sprite = 詳細資料.道具圖片;
        道具名稱.text = 詳細資料.道具名稱 +  "\r\n剩餘: "  + 道具資料.count;
        道具敘述.text = 詳細資料.道具敘述;
        使用按鈕透明度.alpha = 詳細資料.可使用 ? 1f : 0.3f;
        刪除按鈕透明度.alpha = 詳細資料.可刪除 ? 1f : 0.3f;
        Open();
    }

    public void 維持顯示()
	{
        是否維持顯示 = true;
	}

    public void 取消顯示()
	{
        if (!是否維持顯示)
		{
            Close();
        }
	}
	#endregion
}
