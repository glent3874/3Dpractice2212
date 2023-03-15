using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Controller
{
	public override void Interact()
	{
		base.Interact();

		if (¤¬°Ê¦¸¼Æ > 1)
		{
			PlayerInfoManager.instance.AddItem(2);
		}
	}
}
