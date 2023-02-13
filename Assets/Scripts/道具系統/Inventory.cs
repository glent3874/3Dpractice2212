using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Windows<Inventory>
{
    [SerializeField] GameObject 格子模板 = null;
    [SerializeField] RectTransform panal = null;
    [SerializeField] 格子[] slots;

    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();
        UpdateUI();
        PlayerInfoManager.instance.道具發生變化 += UpdateUI;

        slots = panal.GetComponentsInChildren<格子>();
    }

    private void OnDisable()
    {
        PlayerInfoManager.instance.道具發生變化 -= UpdateUI;
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
        Time.timeScale = 1f - Mathf.Clamp01(Inventory.ins.alpha * 0.9f + PauseMenu.ins.alpha);
    }

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

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            //目前的格子(從0) < 存檔中玩家身上的不同道具數 => 接收資料 
            if (i < SaveManager.instance.saveData.stuffs.Count)
            {
                //傳送存檔中的道具資料(id,數量)給 格子
                slots[i].接收資料(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                slots[i].清除資料();
            }
        }
    }
}