using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTable : MonoBehaviour, Item
{
    #region ���
    [SerializeField] GameObject handLightOnTable = null;
    [SerializeField] GameObject handLightOnHand = null;

    bool playerHandling;
    #endregion

    #region �ƥ�

    #endregion

    #region ��k
    public void interact()
    {
        //Ū�����y������A
        playerHandling = handLightOnHand.activeSelf;

        //�ഫ���A
        playerHandling = !playerHandling;

        handLightOnHand.SetActive(playerHandling);
        handLightOnTable.SetActive(!playerHandling);
    }
    #endregion
    
}
