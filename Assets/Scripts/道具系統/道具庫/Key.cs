using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_��
/// </summary>
public class Key : MonoBehaviour, Interactable
{
    #region ���
    [SerializeField] NpcData �奻 = null;
    [SerializeField] string saveKey = "�G���_��";

    int ���ʦ��� = 0;
	#endregion

<<<<<<< HEAD
	#region �ƥ�
	private void Start()
	{
        //�V���a��ƺ޲z���d���_�ͬO���O�Q�ߤF
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
            Destroy(this.gameObject);
	}
	#endregion
=======
	private void Start()
	{
		if (PlayerInfoManager.instance.GetBool(saveKey) == true)
		{
            Destroy(this.gameObject);
		}
	}
>>>>>>> c710b460939240fae837926da5c6d09db74b3b2c

	#region ��k
	/// <summary>
	/// ����
	/// </summary>
	public void Interact()
    {
        if(���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�奻, this.transform.position);
        }
        if(���ʦ��� == 1)
        {
            PlayerInfoManager.instance.AddItem(0);

            PlayerInfoManager.instance.SetBool(saveKey, true);

            Destroy(this.gameObject);
        }
        ���ʦ���++;
    }
    #endregion
}
