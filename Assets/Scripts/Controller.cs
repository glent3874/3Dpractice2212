using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i����
/// �}����������
/// </summary>
public class Controller : MonoBehaviour, Item
{
    #region ���
    [SerializeField] Animator door;                 //�}���ʵe
    [SerializeField] NpcData �y�z�奻 = null;       //�y�z�奻

    int ���ʦ��� = 0;
    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    public void interact()
    {
        //�Ĥ@�����ʴy�z����
        //�ĤG���}���ò���
        if (���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�y�z�奻, this.transform.position);
        }
        if (���ʦ��� == 1)
        { 
            door.SetBool("�}��", true);
            Destroy(this.gameObject);
        }
        ���ʦ���++;
    }
    #endregion
}
