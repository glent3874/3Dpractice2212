using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 遊戲開始的操作說明
/// </summary>
public class StartManual : MonoBehaviour
{
    #region 欄位
    [SerializeField] GameObject 開始說明 = null;
    [SerializeField] NpcData 開始文本 = null;
    [SerializeField] Text 標題 = null;
    [SerializeField] Text 內文 = null;
    [SerializeField] GameObject 繼續提示 = null;

    public bool isPlay = false;
    #endregion

    #region 事件
    private void Start()
    {
        開始對話(開始文本);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && stop)
        {
            stop = false;
        }
    }
    #endregion

    #region 方法
    public void 開始對話(NpcData 文本)
    {
        if (isPlay) return;
        StartCoroutine(說明中(文本));
    }
    IEnumerator 說明中(NpcData 文本)
    {
        isPlay = true;
        內文.text = "";
        標題.text = "";
        stop = false;

        for (int i = 0; i < 文本.對話列表.Count; i++)
        {
            標題.text = 文本.對話列表[i].講者;

            string 輸出 = "";

            for (int j = 0; j < 文本.對話列表[i].內容.Length; j++) 
            {
                char 新的字 = 文本.對話列表[i].內容[j];
                輸出 = 輸出 + 新的字;

                內文.text = 輸出;
                yield return new WaitForSeconds(0.05f);
            }
            stop = true;
            while (stop)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        isPlay = false;
        開始說明.SetActive(false);
    }

    bool stop
    {
        get { return _stop; }
        set
        {
            _stop = value;
            if (_stop)
            {
                繼續提示.SetActive(true);
            }
            else
            {
                繼續提示.SetActive(false);
            }
        }
    }
    bool _stop = false;
    #endregion
}
