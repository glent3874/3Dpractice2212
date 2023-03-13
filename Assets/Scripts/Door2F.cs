using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2F: MonoBehaviour, Interactable
{
	[SerializeField] Animator �����ʵe = null;
	[SerializeField] NpcData �S�_�ͪ��@�� = null;
	[SerializeField] NpcData ���_�ͪ��@�� = null;
	[SerializeField] int �_��ID = 0;
	[SerializeField] string saveKey = "2F��";

	private void Start()
	{
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
			�����ʵe.SetBool("�}��", true);
	}

	public void Interact()
	{
		if (PlayerInfoManager.instance.�O�_��(�_��ID))
		{
			��ܨt��.instance.�}�l���(���_�ͪ��@��);
			�����ʵe.SetBool("�}��", true);

			PlayerInfoManager.instance.SetBool(saveKey, true);
		}
		else
		{
			��ܨt��.instance.�}�l���(�S�_�ͪ��@��);
		}
	}
}