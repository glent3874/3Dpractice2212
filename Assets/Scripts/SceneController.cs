using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������
/// </summary>
public class SceneController : MonoBehaviour
{
    #region ��k
    /// <summary>
    /// ����@����J����
    /// </summary>
    public void LoadGameScene1()
    {
        Invoke("DelayLoadGameScene1", 1);
    }
    
    /// <summary>
    /// ���J����
    /// </summary>
    private void DelayLoadGameScene1()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// ���}�C��
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
