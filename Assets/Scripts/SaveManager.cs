using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �s�ɸ�ƺ޲z��
/// </summary>
public class SaveManager
{
    #region ��ҼҦ�
    public static SaveManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }
    static SaveManager _instance = null;
    #endregion

    #region ���
    public PlayerData saveData;
    public bool dataExist = false;
    public bool continueGame = false;
    #endregion

    #region ��k
    /// <summary>
    /// �U���ɮ�
    /// </summary>
    public void Download()
    {
        //�q�w��Ū����� �p�G�S����ƴN�^��"N"
        string json = PlayerPrefs.GetString("PLAYER_DATA", "N");

        //�p�G����ƴN�ѪR
        if (json != "N") 
        {
            //��json��r��^���a���
            saveData = JsonUtility.FromJson<PlayerData>(json);
            dataExist = true;
        }
        else
        {
            //�S���
            //���t�O���鵹�s�ɸ��
            saveData = new PlayerData();
            dataExist = false;
            saveData.stuffs = new List<Stuff>();
        }
    }

    /// <summary>
    /// �W���ɮ�
    /// </summary>
    public void Upload()
    {
        //�⪱�a����নjson��r
        string json = JsonUtility.ToJson(saveData, true);
        Debug.Log(json);
        //������w�Ф� �R�W��PLAYER_DATA
        PlayerPrefs.SetString("PLAYER_DATA", json);
    }
    #endregion
}

#region ���c
[System.Serializable]
public struct PlayerData
{
    public string levelName;            //��e���d
    public Vector3 playerPos;           //���a��m
    public Vector3 playerRotateY;       //���a���V
    public List<Stuff> stuffs;          //���a���W���D����
}
#endregion