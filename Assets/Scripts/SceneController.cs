using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 場景控制器
/// </summary>
public class SceneController : MonoBehaviour
{
    #region 欄位
    [SerializeField] Text continueText = null;
    [SerializeField] Color continueTextColor = Color.gray;
    #endregion

    #region 事件
    private void Awake()
    {
        SaveManager.instance.Download();                                                        //要求存檔系統下載檔案
        if (SaveManager.instance.dataExist == false) continueText.color = continueTextColor;    //存檔不存在就取消continue
    }
    #endregion

    #region 方法
    /// <summary>
    /// 新遊戲
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //載入menu後第一個場景
    }

    /// <summary>
    /// 繼續遊戲
    /// </summary>
    public void Continue()
    {
        if (SaveManager.instance.dataExist == false) return;    //存檔不存在就無法使用
        SaveManager.instance.continueGame = true;               //需要載入存檔
        SceneManager.LoadScene(SaveManager.instance.saveData.levelName);            //載入存檔中所在關卡
    }

    /// <summary>
    /// 關於作者
    /// </summary>
    public void About()
    {
        Application.OpenURL("https://www.instagram.com/glent_3874/?next=%2F");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// 載入場景:New
    /// </summary>
    private void DelayLoadNewGameScene1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //載入menu後第一個場景
    }

    /// <summary>
    /// 載入場景:Continue
    /// </summary>
    private void DelayLoadContinueGameScene()
    {
        SceneManager.LoadScene(SaveManager.instance.saveData.levelName);            //載入存檔中所在關卡
    }
    #endregion
}
