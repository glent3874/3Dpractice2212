using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Ȱ����
/// </summary>
public class PauseMenu : Windows<PauseMenu>
{
    #region �ƥ�
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
        Time.timeScale = 1f - alpha;                        //�C���t���H�z���׺��C�ܰ���
    }
    #endregion

    #region ��k
    /// <summary>
    /// ����}�Ү�
    /// </summary>
    public override void Open()
    {
        base.Open();
        Cursor.lockState = CursorLockMode.None;             //����ƹ�
    }

    /// <summary>
    /// ����������
    /// </summary>
    public override void Close()
    {
        base.Close();
        Cursor.lockState = CursorLockMode.Locked;           //��w�ƹ�
    }

    /// <summary>
    /// �^��C��
    /// </summary>
    public void Resume()
    {
        if (isOpen) Close();                                //�Y���O�}�Ҫ��A�������
    }

    /// <summary>
    /// �s��
    /// </summary>
    public void Save()
    {
        SaveManager.instance.saveData.levelName = SceneManager.GetActiveScene().name;                   //�O���������W�r
        SaveManager.instance.saveData.playerPos = Player.instance.transform.position;                   //�O�����a��m
        SaveManager.instance.saveData.playerRotateY = Player.instance.��������b.rotation.eulerAngles;    //�O�����a��V
        SaveManager.instance.Upload();                                                                  //�n�D�t�ΤW���ɮ�
    }

    /// <summary>
    /// �ﶵ
    /// </summary>
    public void Setting()
    {

    }
    
    /// <summary>
    /// �^��ؿ�
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
