using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, Item
{
    #region ���
    [SerializeField] Animator door;
    #endregion

    #region �ƥ�
    #endregion

    #region ��k
    public void interact()
    {
        door.SetBool("�}��", true);
        Destroy(this.gameObject);
    }
    #endregion
}
