using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��m���y���O�x
/// </summary>
public class LightTable : MonoBehaviour, Item
{
    #region ���
    [SerializeField] GameObject handLightOnTable = null;    //��W�����y
    [SerializeField] GameObject handLightOnHand = null;     //��W�����y
    [SerializeField] NpcData �y�z�奻 = null;               //�O�x�奻

    int ���ʦ��� = 0;
    bool playerHandling;
    #endregion

    #region ��k
    /// <summary>
    /// ����
    /// </summary>
    public void interact()
    {
        //�Ĥ@�����ʴy�z
        //�ĤG���H���m&���_���y
        if(���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�y�z�奻, this.transform.position);
        }

        if(���ʦ��� != 0)
        {
            //Ū�����y���A
            playerHandling = handLightOnHand.activeSelf;

            //�ഫ���A
            playerHandling = !playerHandling;

            handLightOnHand.SetActive(playerHandling);
            handLightOnTable.SetActive(!playerHandling);
        }
        ���ʦ���++;
    }
    #endregion
    
}
