using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : AYEMonster<�a�H�欰>
{
	[SerializeField] GameObject[] �i�����I = new GameObject[0];
	[SerializeField] Transform ���� = null;
	[SerializeField] LayerMask �|�ϵ��u�������ϼh;
	[SerializeField] float ���u�Z�� = 8f;
	[SerializeField] float ���u���� = 60f;
	Transform �ڪ��ĤH = null;

	int �����I�s�� = 0;
	bool ��B�ݬݪ��n�_�� = true;
	float �y���ɶ� = 1f;

	private void Awake()
	{
		AddStatus(�a�H�欰.�ݾ�, �ݾ�, ��s�ݾ�, ���}�ݾ�);
		AddStatus(�a�H�欰.����, ����, ��s����, ���}����);
		AddStatus(�a�H�欰.���H, ���H, ��s���H, ���}���H);
		AddStatus(�a�H�欰.�l�H, �l�H, ��s�l�H, ���}�l�H);

		�i�����I = GameObject.FindGameObjectsWithTag("AIPoint");
	}

	#region �ݾ�
	void �ݾ�()
	{
		��B�ݬݪ��n�_�� = true;
	}

	void ��s�ݾ�()
	{
		if (IsTime(Random.Range(1f, 4f)))
		{
			status = �a�H�欰.����;
		}

		if (statusTime > 1f && ��B�ݬݪ��n�_�� == true)
		{
			Face(FindCanSeeTag(0f, ����, "AIPoint", �|�ϵ��u�������ϼh, 20f, 360f));
			��B�ݬݪ��n�_�� = false;
		}

		Transform �ĤH = FindCanSeeTag(0.1f, ����, "PlayerEye", �|�ϵ��u�������ϼh, ���u�Z��, ���u����);
		if (�ĤH != null)
		{
			�ڪ��ĤH = �ĤH;
			status = �a�H�欰.���H;
		}
	}

	void ���}�ݾ�()
	{
		CancelFace();
	}
	#endregion

	#region ����
	void ����()
	{
		�����I�s�� = Random.Range(0, �i�����I.Length);
		anim.SetBool("��", true);
	}

	void ��s����()
	{
		Vector3 �ؼЦ�m = �i�����I[�����I�s��].transform.position;
		Way(�ؼЦ�m);
		if (statusTime > 20f)
			status = �a�H�欰.�ݾ�;
		if (Close(�ؼЦ�m, true))
			status = �a�H�欰.�ݾ�;

		Transform �ĤH = FindCanSeeTag(0.1f, ����, "PlayerEye", �|�ϵ��u�������ϼh, ���u�Z��, ���u����);
		if (�ĤH != null)
		{
			�ڪ��ĤH = �ĤH;
			status = �a�H�欰.���H;
		}
	}

	void ���}����()
	{
		anim.SetBool("��", false);
	}
	#endregion

	#region ���H
	void ���H()
	{
		Face(�ڪ��ĤH);
	}

	void ��s���H()
	{
		if (statusTime > 1f)
		{
			status = �a�H�欰.�l�H;
		}
	}

	void ���}���H()
	{
		CancelFace();
	}
	#endregion

	#region �l�H
	void �l�H()
	{
		�y���ɶ� = 1f;
	}

	void ��s�l�H()
	{
		Way(�ڪ��ĤH.position);
		if (CanSee(����.position, �ڪ��ĤH.position, �|�ϵ��u�������ϼh))
			�y���ɶ� = 1f;
		else
			�y���ɶ� -= Time.deltaTime;
		if (�y���ɶ� <= 0f)
		{
			status = �a�H�欰.�ݾ�;
		}
	}

	void ���}�l�H()
	{

	}
	#endregion
}

public enum �a�H�欰
{
    �ݾ�,
    ����,
	���H,
	�l�H,
}