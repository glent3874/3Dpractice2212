using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i����
/// �}����������
/// </summary>
public class Controller : MonoBehaviour, Interactable
{
    #region ���
    [SerializeField] Animator door = null;                 //�}���ʵe
    [SerializeField] NpcData �y�z�奻 = null;       //�y�z�奻
    [SerializeField] Animator �O�x = null;
    [SerializeField] GameObject �O�x�� = null;

    public int ���ʦ��� = 0;
	#endregion

	#region ��k
	/// <summary>
	/// ����
	/// </summary>
	public virtual void Interact()
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
            �O�x.SetBool("����", true);
            �O�x��.SetActive(false);

            Destroy(this.gameObject);
        }
        ���ʦ���++;
    }
    #endregion
}
