using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ĤHAI
/// </summary>
public class Monster : AYEMonster<�a�H�欰>
{
    #region ���
    [SerializeField] GameObject[] �i�����I = new GameObject[0];			//AI�n�e�����I
	[SerializeField] Transform ���� = null;
	[SerializeField] LayerMask �|�ϵ��u�������ϼh;
	[SerializeField] float ���u�Z�� = 8f;
	[SerializeField] float ���u���� = 60f;
	[SerializeField] float �����Z�� = 1.5f;
	[SerializeField] float �������� = 90f;
	[SerializeField] float ���q�����O = 1f;
	[SerializeField] [Range(0.1f, 3f)] float �`�N�O = 0.5f;
	Transform �ڪ��ĤH = null;

	int �����I�s�� = 0;
	bool ��B�ݬݪ��n�_�� = true;
	float �y���ɶ� = 0f;
	Vector3 �̫ᨣ�쪱�a����m = Vector3.zero;
    #endregion

    #region �ƥ�
    private void Awake()
	{
		//�n�����A
		AddStatus(�a�H�欰.�ݾ�, �ݾ�, ��s�ݾ�, ���}�ݾ�);
		AddStatus(�a�H�欰.����, ����, ��s����, ���}����);
		AddStatus(�a�H�欰.���H, ���H, ��s���H, ���}���H);
		AddStatus(�a�H�欰.�l�H, �l�H, ��s�l�H, ���}�l�H);
		AddStatus(�a�H�欰.����, ����, ��s����, ���}����);

		�i�����I = GameObject.FindGameObjectsWithTag("AIPoint");			//��i�H�����I
	}
    #endregion

    #region ���A
    #region �ݾ�
    void �ݾ�()
	{
		��B�ݬݪ��n�_�� = true;
	}

	void ��s�ݾ�()
	{
		//�ݾ��@�q�ɶ��N����
		if (IsTime(Random.Range(1f, 4f)))
		{
			status = �a�H�欰.����;
		}

		if (statusTime > 0.7f && ��B�ݬݪ��n�_�� == true)
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
		if (statusTime > 0.7f)
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
		anim.SetBool("�]", true);
		�y���ɶ� = 1f;
	}

	void ��s�l�H()
	{
		//���b�l�H�N���_��s�n�e������m �Y���a����m
		//�Y�ٯ�ݨ��ĤH�N����`�N�O
		//�������u�@�q�ɶ�(�`�N�O) �N�|���_��s��m
		//�F��AI�������a���s����h�V���ĪG
		if (�y���ɶ� > 0f)
			�̫ᨣ�쪱�a����m = �ڪ��ĤH.position;
		if (CanSee(����.position, �ڪ��ĤH.position, �|�ϵ��u�������ϼh))
			�y���ɶ� = �`�N�O;
		else
			�y���ɶ� -= Time.deltaTime;

		Way(�̫ᨣ�쪱�a����m);

		if (Close(�̫ᨣ�쪱�a����m, true, �����Z��))
		{
			if (Vector3.Distance(�̫ᨣ�쪱�a����m, �ڪ��ĤH.position) < 0.1f)
			{
				status = �a�H�欰.����;
			}
			else
			{
				status = �a�H�欰.�ݾ�;
			}
		}

		if (statusTime > 30f)
			status = �a�H�欰.�ݾ�;
	}

	void ���}�l�H()
	{
		anim.SetBool("�]", false);
	}
	#endregion

	#region ����
	void ����()
	{
		anim.SetTrigger("����");
	}

	void ��s����()
	{
		if (statusTime < 0.5f)
			Way(�ڪ��ĤH.position);

		if (statusTime > 0.95f)
		{
			status = �a�H�欰.�ݾ�;
		}
	}

	void ���}����()
	{
		anim.ResetTrigger("����");
	}

	public void �ʵe�����ɾ�()
	{
		���Χ���(�����Z��, ��������, ���q�����O);
	}
    #endregion
    #endregion

    #region �\��
    void ���Χ���(float �Z��,float ����,float �����O)
	{
		//�����P�򪫥󪺸I����
		Collider[] �P�䪺�I���� = Physics.OverlapSphere(transform.TransformPoint(0f, 0f, -1f), �Z�� + 1f); ;
		for (int i = 0; i < �P�䪺�I����.Length; i++)
		{
			//�p�G�O�ۤv���W���N���L
			if (�P�䪺�I����[i].transform.GetInstanceID() == this.transform.GetInstanceID())
				continue;

			//�p�⦹���󪺦�m�O�_�b�ڪ������d��
			Vector3 c = �P�䪺�I����[i].transform.position;
			c.y = this.transform.position.y;
			Vector3 ab = this.transform.forward;
			Vector3 ac = c - this.transform.position;
			float ���� = Vector3.Angle(ab, ac);
			if (���� < ���� * 0.5f)
			{
				//�p�G�����󨭤W���ͩR�Ҷ� �Y�i���� �N�Ϩ���
				�ͩR�Ҷ� �i�઺�ͩR�Ҷ� = �P�䪺�I����[i].gameObject.GetComponent<�ͩR�Ҷ�>();
				if (�i�઺�ͩR�Ҷ� != null)
				{
					�i�઺�ͩR�Ҷ�.����(�����O, this.transform.position);
				}
			}
		}
	}
	#endregion
}

#region ���c
public enum �a�H�欰
{
    �ݾ�,
    ����,
	���H,
	�l�H,
	����,
}
#endregion
