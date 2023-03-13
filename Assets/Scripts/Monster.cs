using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : AYEMonster<壞人行為>
{
	[SerializeField] GameObject[] 可走的點 = new GameObject[0];
	[SerializeField] Transform 眼睛 = null;
	[SerializeField] LayerMask 會使視線受阻的圖層;
	[SerializeField] float 視線距離 = 8f;
	[SerializeField] float 視線角度 = 60f;
	Transform 我的敵人 = null;

	int 巡邏點編號 = 0;
	bool 到處看看的好奇心 = true;
	float 獵殺時間 = 1f;

	private void Awake()
	{
		AddStatus(壞人行為.待機, 待機, 刷新待機, 離開待機);
		AddStatus(壞人行為.巡邏, 巡邏, 刷新巡邏, 離開巡邏);
		AddStatus(壞人行為.見人, 見人, 刷新見人, 離開見人);
		AddStatus(壞人行為.追人, 追人, 刷新追人, 離開追人);

		可走的點 = GameObject.FindGameObjectsWithTag("AIPoint");
	}

	#region 待機
	void 待機()
	{
		到處看看的好奇心 = true;
	}

	void 刷新待機()
	{
		if (IsTime(Random.Range(1f, 4f)))
		{
			status = 壞人行為.巡邏;
		}

		if (statusTime > 1f && 到處看看的好奇心 == true)
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
		if (statusTime > 1f)
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
		獵殺時間 = 1f;
	}

	void 刷新追人()
	{
		Way(我的敵人.position);
		if (CanSee(眼睛.position, 我的敵人.position, 會使視線受阻的圖層))
			獵殺時間 = 1f;
		else
			獵殺時間 -= Time.deltaTime;
		if (獵殺時間 <= 0f)
		{
			status = 壞人行為.待機;
		}
	}

	void 離開追人()
	{

	}
	#endregion
}

public enum 壞人行為
{
    待機,
    巡邏,
	見人,
	追人,
}