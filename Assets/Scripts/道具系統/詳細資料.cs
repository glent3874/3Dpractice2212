using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 詳細資料 : Windows<詳細資料>
{
    [SerializeField] Image 圖示 = null;
    [SerializeField] Text 道具名稱 = null;
    [SerializeField] Text 道具敘述 = null;
    [SerializeField] CanvasGroup 使用按鈕透明度 = null;
    [SerializeField] CanvasGroup 刪除按鈕透明度 = null;

    bool 是否維持顯示 = false;

    public void 顯示(Stuff 道具資料)
    {
        StuffData 詳細資料 = StuffManager.instance.GetDataById(道具資料.id);
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
}
