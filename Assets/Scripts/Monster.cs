using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵人AI
/// </summary>
public class Monster : AYEMonster<壞人行為>
{
    #region 欄位
    [SerializeField] GameObject[] 可走的點 = new GameObject[0];			//AI要前往的點
	[SerializeField] Transform 眼睛 = null;
	[SerializeField] LayerMask 會使視線受阻的圖層;
	[SerializeField] float 視線距離 = 8f;
	[SerializeField] float 視線角度 = 60f;
	[SerializeField] float 攻擊距離 = 1.5f;
	[SerializeField] float 攻擊角度 = 90f;
	[SerializeField] float 普通攻擊力 = 1f;
	[SerializeField] [Range(0.1f, 3f)] float 注意力 = 0.5f;
	Transform 我的敵人 = null;

	int 巡邏點編號 = 0;
	bool 到處看看的好奇心 = true;
	float 獵殺時間 = 0f;
	Vector3 最後見到玩家的位置 = Vector3.zero;
    #endregion

    #region 事件
    private void Awake()
	{
		//登錄狀態
		AddStatus(壞人行為.待機, 待機, 刷新待機, 離開待機);
		AddStatus(壞人行為.巡邏, 巡邏, 刷新巡邏, 離開巡邏);
		AddStatus(壞人行為.見人, 見人, 刷新見人, 離開見人);
		AddStatus(壞人行為.追人, 追人, 刷新追人, 離開追人);
		AddStatus(壞人行為.攻擊, 攻擊, 刷新攻擊, 離開攻擊);

		可走的點 = GameObject.FindGameObjectsWithTag("AIPoint");			//抓可以走的點
	}
    #endregion

    #region 狀態
    #region 待機
    void 待機()
	{
		到處看看的好奇心 = true;
	}

	void 刷新待機()
	{
		//待機一段時間就巡邏
		if (IsTime(Random.Range(1f, 4f)))
		{
			status = 壞人行為.巡邏;
		}

		if (statusTime > 0.7f && 到處看看的好奇心 == true)
		{
			Face(FindCanSeeTag(0f, 眼睛, "AIPoint", 會使視線受阻的圖層, 20f, 360f));
			到處看看的好奇心 = false;
		}

		Transform 敵人 = FindCanSeeTag(0.1f, 眼睛, "PlayerEye", 會使視線受阻的圖層, 視線距離, 視線角度);
		if (敵人 != null)
		{
			我的敵人 = 敵人;
			status = 壞人行為.見人;
		}
	}

	void 離開待機()
	{
		CancelFace();
	}
	#endregion

	#region 巡邏
	void 巡邏()
	{
		巡邏點編號 = Random.Range(0, 可走的點.Length);
		anim.SetBool("走", true);
	}

	void 刷新巡邏()
	{
		Vector3 目標位置 = 可走的點[巡邏點編號].transform.position;
		Way(目標位置);
		if (statusTime > 20f)
			status = 壞人行為.待機;
		if (Close(目標位置, true))
			status = 壞人行為.待機;

		Transform 敵人 = FindCanSeeTag(0.1f, 眼睛, "PlayerEye", 會使視線受阻的圖層, 視線距離, 視線角度);
		if (敵人 != null)
		{
			我的敵人 = 敵人;
			status = 壞人行為.見人;
		}
	}

	void 離開巡邏()
	{
		anim.SetBool("走", false);
	}
	#endregion

	#region 見人
	void 見人()
	{
		Face(我的敵人);
	}

	void 刷新見人()
	{
		if (statusTime > 0.7f)
		{
			status = 壞人行為.追人;
		}
	}

	void 離開見人()
	{
		CancelFace();
	}
	#endregion

	#region 追人
	void 追人()
	{
		anim.SetBool("跑", true);
		獵殺時間 = 1f;
	}

	void 刷新追人()
	{
		//當正在追人就不斷刷新要前往的位置 即玩家的位置
		//若還能看見敵人就持續注意力
		//脫離視線一段時間(注意力) 就會中斷刷新位置
		//達成AI推測玩家轉彎之後去向的效果
		if (獵殺時間 > 0f)
			最後見到玩家的位置 = 我的敵人.position;
		if (CanSee(眼睛.position, 我的敵人.position, 會使視線受阻的圖層))
			獵殺時間 = 注意力;
		else
			獵殺時間 -= Time.deltaTime;

		Way(最後見到玩家的位置);

		if (Close(最後見到玩家的位置, true, 攻擊距離))
		{
			if (Vector3.Distance(最後見到玩家的位置, 我的敵人.position) < 0.1f)
			{
				status = 壞人行為.攻擊;
			}
			else
			{
				status = 壞人行為.待機;
			}
		}

		if (statusTime > 30f)
			status = 壞人行為.待機;
	}

	void 離開追人()
	{
		anim.SetBool("跑", false);
	}
	#endregion

	#region 攻擊
	void 攻擊()
	{
		anim.SetTrigger("攻擊");
	}

	void 刷新攻擊()
	{
		if (statusTime < 0.5f)
			Way(我的敵人.position);

		if (statusTime > 0.95f)
		{
			status = 壞人行為.待機;
		}
	}

	void 離開攻擊()
	{
		anim.ResetTrigger("攻擊");
	}

	public void 動畫攻擊時機()
	{
		扇形攻擊(攻擊距離, 攻擊角度, 普通攻擊力);
	}
    #endregion
    #endregion

    #region 功能
    void 扇形攻擊(float 距離,float 角度,float 攻擊力)
	{
		//捕捉周圍物件的碰撞器
		Collider[] 周邊的碰撞器 = Physics.OverlapSphere(transform.TransformPoint(0f, 0f, -1f), 距離 + 1f); ;
		for (int i = 0; i < 周邊的碰撞器.Length; i++)
		{
			//如果是自己身上的就跳過
			if (周邊的碰撞器[i].transform.GetInstanceID() == this.transform.GetInstanceID())
				continue;

			//計算此物件的位置是否在我的攻擊範圍內
			Vector3 c = 周邊的碰撞器[i].transform.position;
			c.y = this.transform.position.y;
			Vector3 ab = this.transform.forward;
			Vector3 ac = c - this.transform.position;
			float 夾角 = Vector3.Angle(ab, ac);
			if (夾角 < 角度 * 0.5f)
			{
				//如果此物件身上有生命模塊 即可扣血 就使受傷
				生命模塊 可能的生命模塊 = 周邊的碰撞器[i].gameObject.GetComponent<生命模塊>();
				if (可能的生命模塊 != null)
				{
					可能的生命模塊.受傷(攻擊力, this.transform.position);
				}
			}
		}
	}
	#endregion
}

#region 結構
public enum 壞人行為
{
    待機,
    巡邏,
	見人,
	追人,
	攻擊,
}
#endregion
