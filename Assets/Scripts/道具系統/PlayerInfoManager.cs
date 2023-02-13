using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    #region ��k
    public void AddItem(int �D��s��)
    {
        bool �K�[���\ = false;
        for (int i = 0; i < SaveManager.instance.saveData.stuffs.Count; i++) 
        {
            if(SaveManager.instance.saveData.stuffs[i].id == �D��s��)
            {
                Stuff temp = SaveManager.instance.saveData.stuffs[i];
                temp.count++;
                SaveManager.instance.saveData.stuffs[i] = temp;
                �K�[���\ = true;
                break;
            }
        }

        if (�K�[���\ == false)
        {
            Stuff temp = new Stuff();
            temp.id = �D��s��;
            temp.count = 1;
            SaveManager.instance.saveData.stuffs.Add(temp);
        }

        if(�D��o���ܤ� != null)
        {
            �D��o���ܤ�.Invoke();
        }
    }
    public void RemoveItem(int �D��s��)
    {
        if (�D��o���ܤ� != null)
        {
            �D��o���ܤ�.Invoke();
        }
    }
    public Action �D��o���ܤ� = null;

    public bool �O�_��(int �D��s��)
    {
        return false;
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
