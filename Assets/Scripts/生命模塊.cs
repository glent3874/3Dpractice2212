using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生命模組 萬有血條
/// </summary>
public class 生命模塊 : MonoBehaviour
{
    #region 欄位
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
    #endregion

    #region 事件
    private void Start()
	{
		hp = maxHp;
	}
    #endregion

    #region 方法
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
    #endregion
}

#region 結構
public struct 傷害包
{
	public float 攻擊力;
	public Vector3 攻擊位置;
}
#endregion
