using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_��
/// </summary>
public class Key : MonoBehaviour, Interactable
{
    #region ���
    [SerializeField] NpcData �奻 = null;

    int ���ʦ��� = 0;
    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    public void Interact()
    {
        if(���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�奻, this.transform.position);
        }
        if(���ʦ��� == 1)
        {
            PlayerInfoManager.instance.AddItem(0);

            Destroy(this.gameObject);
        }
        ���ʦ���++;
    }
    #endregion
}
