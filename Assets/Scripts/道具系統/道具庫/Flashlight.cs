using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Controller
{
    [SerializeField] string saveKey = "2F傳送門";

	private void Start()
	{
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
		{
			Destroy(this.gameObject);
		}
	}

	public override void Interact()
	{
		base.Interact();

		if (互動次數 > 1)
		{
			PlayerInfoManager.instance.AddItem(2);

			PlayerInfoManager.instance.SetBool(saveKey, true);
		}
	}
}
