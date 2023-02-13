using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : Windows<Inventary>
{
    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();
        UpdateUI();
        PlayerInfoManager.instance.道具發生變化 += UpdateUI;
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
        Time.timeScale = 1f - Mathf.Clamp01(Inventary.ins.alpha * 0.9f + PauseMenu.ins.alpha);
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

    [SerializeField] GameObject 格子模板 = null;
    [SerializeField] GameObject Panal = null;
    [SerializeField] RectTransform 背景圖 = null;

    void UpdateUI()
    {
        格子模板.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            //產生新格子並丟在Panal裡 Panal會自行排列
            GameObject 新格子 = Instantiate(格子模板, 背景圖);
            新格子.SetActive(true);

            //目前的格子(從0) < 存檔中玩家身上的不同道具數 => 接收資料 
            if (i < SaveManager.instance.saveData.stuffs.Count)
            {
                新格子.GetComponent<格子>().接收資料(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                //把格子模板裡的圖片與數量縮成0
                for (int j = 0; j < 新格子.transform.childCount; j++)
                {
                    新格子.transform.GetChild(j).localScale = Vector3.zero;
                }
            }
        }
    }
}
