using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2F: MonoBehaviour, Interactable
{
	[SerializeField] Animator 門的動畫 = null;
	[SerializeField] NpcData 沒鑰匙的劇情 = null;
	[SerializeField] NpcData 有鑰匙的劇情 = null;
	[SerializeField] int 鑰匙ID = 0;
	[SerializeField] string saveKey = "2F門";

	private void Start()
	{
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
			門的動畫.SetBool("開關", true);
	}

	public void Interact()
	{
		if (PlayerInfoManager.instance.是否有(鑰匙ID))
		{
			對話系統.instance.開始對話(有鑰匙的劇情);
			門的動畫.SetBool("開門", true);

			PlayerInfoManager.instance.SetBool(saveKey, true);
		}
		else
		{
			對話系統.instance.開始對話(沒鑰匙的劇情);
		}
	}
}
