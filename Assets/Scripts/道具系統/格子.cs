using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 格子 : MonoBehaviour
{
    [SerializeField] Image 圖示 = null;
    [SerializeField] Text 標題 = null;

    public void 接收資料(Stuff 我要呈現的內容)
    {
        StuffData 詳細的道具資料 = StuffManager.instance.GetDataById(我要呈現的內容.id);
        圖示.sprite = 詳細的道具資料.道具圖片;
        標題.text = 我要呈現的內容.count + "";
    }
}
