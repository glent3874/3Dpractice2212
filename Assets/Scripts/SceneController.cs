using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
public class SceneController : MonoBehaviour
{
    #region ���
    [SerializeField] Text continueText = null;
    [SerializeField] Color continueTextColor = Color.gray;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        Time.timeScale = 1f;
        SaveManager.instance.Download();                                                        //�n�D�s�ɨt�ΤU���ɮ�
        if (SaveManager.instance.dataExist == false) continueText.color = continueTextColor;    //�s�ɤ��s�b�N����continue
    }
    #endregion

    #region ��k
    /// <summary>
    /// �s�C��
    /// </summary>
    public void NewGame()
    {
        SaveManager.instance.�M�����();

        �����.ins.���(�¹����U�Ӥ���);
    }

    /// <summary>
    /// �e�����ƥ�
    /// </summary>
    void �¹����U�Ӥ���()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //���Jmenu��Ĥ@�ӳ���
    }

    /// <summary>
    /// �~��C��
    /// </summary>
    public void Continue()
    {
        if (SaveManager.instance.dataExist == false) return;    //�s�ɤ��s�b�N�L�k�ϥ�
        SaveManager.instance.continueGame = true;               //�ݭn���J�s��
        �����.ins.���(SaveManager.instance.saveData.levelName);
    }

    /// <summary>
    /// ����@��
    /// </summary>
    public void About()
    {
        Application.OpenURL("https://www.instagram.com/glent_3874/?next=%2F");
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
