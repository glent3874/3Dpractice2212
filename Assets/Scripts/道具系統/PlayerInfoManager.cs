using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �s�����a���W���D���T(�u���D�s�F����, ���X��)
/// ���
/// </summary>
public class PlayerInfoManager
{
    #region ��ҼҦ�
    public static PlayerInfoManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PlayerInfoManager();
            }
            return _instance;
        }
    }
    static PlayerInfoManager _instance = null;
    #endregion

    #region ���
    public Action �D��o���ܤ� = null;
    #endregion

    #region ��k
    /// <summary>
    /// ��o�D��
    /// </summary>
    /// <param name="�D��s��"></param>
    public void AddItem(int �D��s��)
    {
        bool �K�[���\ = false;
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
        {
            // �j�M��Ʈw�����S���o�ӹD��
            if(SaveManager.instance.saveData.stuffs[i].id == �D��s��)
            {
                //���ઽ�����ƭק�, �ҥH���X��ק�A�s�^�h
                Stuff temp = SaveManager.instance.saveData.stuffs[i];
                temp.count++;
                SaveManager.instance.saveData.stuffs[i] = temp;
                �K�[���\ = true;
                break;
            }
        }

        //��o�S����o�L���D��N�إ߷s���D���Ʀs��
        if (�K�[���\ == false)
        {
            Stuff temp = new Stuff();
            temp.id = �D��s��;
            temp.count = 1;
            SaveManager.instance.saveData.stuffs.Add(temp);
        }

        // �D���� Inventary �n�O���e��: ��sUI
        // �b���\�s��ƫᰵ����
        if(�D��o���ܤ� != null)
        {
            �D��o���ܤ�.Invoke();
        }
    }

    /// <summary>
    /// �ϥ� �� �����D��
    /// </summary>
    /// <param name="�D��s��"></param>
    public void RemoveItem(int �D��s��)
    {
        // �D���� Inventary �n�O���e��: ��sUI
        // �b���\�s��ƫᰵ����
        if (�D��o���ܤ� != null)
        {
            �D��o���ܤ�.Invoke();
        }
    }
    
    /// <summary>
    /// �j�M���a���W�O�_�������D��
    /// </summary>
    /// <param name="�D��s��"></param>
    /// <returns></returns>
    public bool �O�_��(int �D��s��)
    {
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
		{
            if (SaveManager.instance.saveData.stuffs[i].id == �D��s��)
			{
                return true;
            }
		}
        return false;
    }

<<<<<<< HEAD
    /// <summary>
    /// �ˬd�����������
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
    public bool GetBool(string key)
	{
        for (int i = 0; i < SaveManager.instance.saveData.sceneDatas.Count; i++)
		{
            if (SaveManager.instance.saveData.sceneDatas[i].key == key)
			{
                return SaveManager.instance.saveData.sceneDatas[i].flag;
			}
		}
        return false;
	}

<<<<<<< HEAD
    /// <summary>
    /// �s�����������
    /// </summary>
    /// <param name="key"></param>
    /// <param name="v"></param>
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
    public void SetBool(string key, bool v)
	{
        SceneData temp = new SceneData();
        temp.key = key;
        temp.flag = v;
        temp.pos = Vector3.zero;
        temp.info = "";

        for (int i = 0; i < SaveManager.instance.saveData.sceneDatas.Count; i++)
		{
<<<<<<< HEAD
            //�p�G�w�g���o����ƴN�л\�L�h
=======
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
            if (SaveManager.instance.saveData.sceneDatas[i].key == key)
			{
                SaveManager.instance.saveData.sceneDatas[i] = temp;
                return;
			}
		}

<<<<<<< HEAD
        SaveManager.instance.saveData.sceneDatas.Add(temp);     //�S����ƴN�ؤ@��
=======
        SaveManager.instance.saveData.sceneDatas.Add(temp);
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c
	}
    #endregion
}

#region ���c
[System.Serializable]
public struct Stuff
{
    public int id;
    public int count;
}
#endregion
