using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 生命模塊 : MonoBehaviour
{
    [SerializeField] float maxHp = 10f;
	[SerializeField] Image 血條 = null; 

    float hp
	{
		get { return _hp; }
		set
		{
			_hp = Mathf.Clamp(value, 0f, maxHp);
			if (血條 != null)
			{
				血條.fillAmount = _hp / maxHp;
			}
		}
	}
	float _hp = 0f;

	private void Start()
	{
		hp = maxHp;
	}

	public void 受傷(float 受到的攻擊力, Vector3 對方的座標)
	{
		hp -= 受到的攻擊力;

		傷害包 本次傷害 = new 傷害包();
		本次傷害.攻擊力 = 受到的攻擊力;
		本次傷害.攻擊位置 = 對方的座標;

		if (hp > 0f)
		{
			this.gameObject.SendMessage("Hit", 本次傷害, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			this.gameObject.SendMessage("Die", 本次傷害, SendMessageOptions.DontRequireReceiver);
		}
	}
}

public struct 傷害包
{
	public float 攻擊力;
	public Vector3 攻擊位置;
}
