using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �q���G�Ӫ���
/// </summary>
public class Door2F: MonoBehaviour, Interactable
{
    #region ���
    [SerializeField] Animator �����ʵe = null;
	[SerializeField] NpcData �S�_�ͪ��@�� = null;
	[SerializeField] NpcData ���_�ͪ��@�� = null;
	[SerializeField] int �_��ID = 0;
	[SerializeField] string saveKey = "2F��";
    #endregion

    #region �ƥ�
    private void Start()
	{
		//���w�g�Q���}? �}��:����
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
			�����ʵe.SetBool("�}��", true);
	}
    #endregion

    #region ��k
	/// <summary>
	/// ����
	/// </summary>
    public void Interact()
	{
		//�p�G���_�ʹN�}��
		if (PlayerInfoManager.instance.�O�_��(�_��ID))
		{
			��ܨt��.instance.�}�l���(���_�ͪ��@��);
			�����ʵe.SetBool("�}��", true);

			PlayerInfoManager.instance.SetBool(saveKey, true);		//�x�s�������A
		}
		else
		{
			��ܨt��.instance.�}�l���(�S�_�ͪ��@��);
		}
	}
    #endregion
}
