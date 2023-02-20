using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �D���ƪ��޲z�t��
/// �Ϯ��]��
/// </summary>
public class StuffManager
{
    #region ��ҼҦ�
    public static StuffManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new StuffManager();
            }
            return _instance;
        }
    }
    static StuffManager _instance = null;
    #endregion

    #region ���
    StuffData[] �Ҧ����D��奻 = new StuffData[0];

    bool isLoad = false;
    #endregion

    #region ��k
    /// <summary>
    /// Ū���D����
    /// </summary>
    public void Load()
    {
        if (isLoad) return;
        isLoad = true;
        //�N Resources ��Ƨ������Ҧ� StuffData ��Ū�i��
        //�O��d�ߨt�Τ�
        �Ҧ����D��奻 = Resources.LoadAll<StuffData>("");
    }

    /// <summary>
    /// �q��Ƥ����o�s��
    /// </summary>
    /// <param name="id">�n�d���D��id</param>
    /// <returns>�^�ǹD�㪺�ԲӸ��</returns>
    public StuffData GetDataById(int id)
    {
        for (int i = 0; i < �Ҧ����D��奻.Length; i++) 
        {
            if(�Ҧ����D��奻[i].�D��s�� == id)
            {
                return �Ҧ����D��奻[i];
            }
        }
        return null;
    }
    #endregion
}
