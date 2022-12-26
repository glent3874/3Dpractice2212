using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 暫停選單
/// </summary>
public class PauseMenu : Windows<PauseMenu>
{
    #region 事件
    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(isOpen == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
        Time.timeScale = 1f - alpha;                        //遊戲速度隨透明度漸慢至停止
    }
    #endregion

    #region 方法
    /// <summary>
    /// 當選單開啟時
    /// </summary>
    public override void Open()
    {
        base.Open();
        Cursor.lockState = CursorLockMode.None;             //解鎖滑鼠
    }

    /// <summary>
    /// 當選單關閉時
    /// </summary>
    public override void Close()
    {
        base.Close();
        Cursor.lockState = CursorLockMode.Locked;           //鎖定滑鼠
    }

    /// <summary>
    /// 回到遊戲
    /// </summary>
    public void Resume()
    {
        if (isOpen) Close();                                //若選單是開啟狀態關閉選單
    }

    /// <summary>
    /// 存檔
    /// </summary>
    public void Save()
    {
        SaveManager.instance.saveData.levelName = SceneManager.GetActiveScene().name;                   //記錄此場景名字
        SaveManager.instance.saveData.playerPos = Player.instance.transform.position;                   //記錄玩家位置
        SaveManager.instance.saveData.playerRotateY = Player.instance.水平旋轉軸.rotation.eulerAngles;    //記錄玩家轉向
        SaveManager.instance.Upload();                                                                  //要求系統上傳檔案
    }

    /// <summary>
    /// 選項
    /// </summary>
    public void Setting()
    {

    }
    
    /// <summary>
    /// 回到目錄
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
