using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 道具欄UI
/// 控制開關與UI的刷新
/// </summary>
public class Inventory : Windows<Inventory>
{
    #region 欄位
    [SerializeField] GameObject 格子模板 = null;
    [SerializeField] RectTransform panal = null;
    [SerializeField] 格子[] slots;
    #endregion

    #region 事件
    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();                                   // 要求圖書館員讀取所有道具資料到系統中
        UpdateUI();                                                     // 開啟道具欄先更新一次
        PlayerInfoManager.instance.道具發生變化 += UpdateUI;             // 委派更新UI的動作到玩家道具系統, 存取或移除資料後做更新UI 的動作

        slots = panal.GetComponentsInChildren<格子>();                  // 取得格子
    }

    private void OnDisable()
    {
        PlayerInfoManager.instance.道具發生變化 -= UpdateUI;             // 退訂委派
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen == true)
                Close();
            else
                Open();
        }
        Time.timeScale = 1f - Mathf.Clamp01(Inventory.ins.alpha * 0.9f + PauseMenu.ins.alpha);      //開啟道具欄不會完全暫停
    }
    #endregion

    #region 方法
    public override void Open()
    {
        base.Open();
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Close()
    {
        base.Close();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 查詢存檔並傳送資料給格子顯示
    /// </summary>
    void UpdateUI()
    {
        // 掃過每個格子
        for (int i = 0; i < slots.Length; i++)
        {
            // 道具存檔中有幾筆資料就傳送資料給格子
            if (i < SaveManager.instance.saveData.stuffs.Count)                 
            {
                //傳送存檔中的道具資料(id,數量)給 格子
                slots[i].接收資料(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                // 還有格子但沒資料了 就清除格子
                slots[i].清除資料();
            }
        }
    }
    #endregion
}