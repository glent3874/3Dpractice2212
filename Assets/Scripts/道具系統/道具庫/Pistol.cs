using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1F ���j
/// </summary>
public class Pistol : Controller
{
    #region ���
    [SerializeField] public GameObject mainCam;
    [SerializeField] string saveKey = "1F�ǰe��";
	#endregion

	#region �ƥ�
	private void Start()
	{
        if (PlayerInfoManager.instance.GetBool(saveKey) == true)
		{
            mainCam.GetComponent<Shoot>().havePistol = true;
            Destroy(this.gameObject);
		}
    }
	#endregion

	#region ��k
	public override void Interact()
    {
        base.Interact();

        if (���ʦ��� > 1)
        {
            mainCam.GetComponent<Shoot>().havePistol = true;

            PlayerInfoManager.instance.AddItem(1);

            PlayerInfoManager.instance.SetBool(saveKey, true);
        }
    }
    #endregion
}
