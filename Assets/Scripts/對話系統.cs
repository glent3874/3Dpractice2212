using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 對話系統
/// </summary>
public class 對話系統 : MonoBehaviour
{
    #region 單例模式
    public static 對話系統 instance = null;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region 欄位
    [SerializeField] Animator 動畫 = null;                //對話系統動畫控制器
    [SerializeField] NpcData 測試用文本 = null;           //測試用文本
    [SerializeField] Text 內文 = null;                    //內容
    [SerializeField] Text 人名 = null;                    //講者
    [SerializeField] Transform 繼續提示 = null;           //繼續提示
    [SerializeField] float 距離多遠時會取消對話 = 5f;      //取消對話最大距離
    [SerializeField] public System.Action 對話結束要委派的事情 = null;

    Vector3 當前的位置 = Vector3.zero;
    float 距離;
    public bool isPlay = false;
    #endregion

    #region 事件
    private void Update()
    {
#if UNITY_EDITOR //如果在Unity編輯器中執行
        //測試
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            開始對話(測試用文本);
        }
#endif
        //按下E繼續
        if (Input.GetKeyDown(KeyCode.E) && stop) 
        {
            stop = false;
        }

        //對話中玩家超出距離取消對話
        if (isPlay)
        {
            距離 = Vector3.Distance(當前的位置, Player.instance.transform.position);
            if (距離 > 距離多遠時會取消對話)
            {
                //StopAllCoroutines();
                StopCoroutine("對話中");
                isPlay = false;
                動畫.SetBool("啟動", false);
            }
        }
    }
    #endregion

    #region 方法
    /// <summary>啟動對話系統但不在乎位置</summary>
    /// <param name="文本"></param>
    public void 開始對話(NpcData 文本)
    {
        開始對話(文本, Player.instance.transform.position);
    }

    /// <summary>啟動對話系統</summary>
    /// <param name="文本">對話內容</param>
    /// <param name="位置">互動物件的位置</param>
    public void 開始對話(NpcData 文本, Vector3 位置)
    {
        if (isPlay) return;             //如果正在對話中就跳出
        當前的位置 = 位置;
        StartCoroutine(對話中(文本));
    }

    /// <summary>
    /// 對話系統協程
    /// </summary>
    /// <param name="文本"></param>
    /// <returns></returns>
    IEnumerator 對話中(NpcData 文本)
    {
        isPlay = true;
        內文.text = "";
        stop = false;
        人名.text = "";
        動畫.SetBool("啟動", true);
        yield return new WaitForSeconds(0.3f);                      //動畫結束再開始印字

        //印字
        //處理一段話
        for (int i = 0; i < 文本.對話列表.Count; i++)
        {
            人名.text = 文本.對話列表[i].講者;

            string 要講的話 = "";

            //處理每個字
            for (int j = 0; j < 文本.對話列表[i].內容.Length; j++)
            {
                char 新的字 = 文本.對話列表[i].內容[j];
                要講的話 = 要講的話 + 新的字;

                內文.text = 要講的話;
                yield return new WaitForSeconds(0.06f);
            }
            //對話跑完停下來
            stop = true;
            while (stop) 
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        動畫.SetBool("啟動", false);
        isPlay = false;

        if(對話結束要委派的事情 != null)
        {
            對話結束要委派的事情.Invoke();
        }
    }

    /// <summary>
    /// 同時設置繼續提示的顯示
    /// </summary>
    bool stop
    {
        get { return _stop; }
        set 
        { 
            _stop = value;
            if (_stop)
            {
                繼續提示.localScale = Vector3.one;
            }
            else
            {
                繼續提示.localScale = Vector3.zero;
            }
        }
    }
    bool _stop = false;
    #endregion
}