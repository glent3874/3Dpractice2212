using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, Item
{
    #region ���
    [SerializeField] Animator door;
    [SerializeField] NpcData �y�z�奻 = null;

    int ���ʦ��� = 0;
    #endregion

    #region �ƥ�

    #endregion

    #region ��k
    public void interact()
    {
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
