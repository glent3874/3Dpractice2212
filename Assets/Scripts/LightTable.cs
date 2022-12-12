using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTable : MonoBehaviour, Item
{
    #region ���
    [SerializeField] GameObject handLightOnTable = null;
    [SerializeField] GameObject handLightOnHand = null;
    [SerializeField] NpcData �y�z�奻 = null;

    int ���ʦ��� = 0;
    bool playerHandling;
    #endregion

    #region �ƥ�

    #endregion

    #region ��k
    public void interact()
    {
        if(���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�y�z�奻, this.transform.position);
        }

        if(���ʦ��� != 0)
        {
            //Ū�����y������A
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
