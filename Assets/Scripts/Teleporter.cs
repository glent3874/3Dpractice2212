using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ǰe��
/// </summary>
public class Teleporter : MonoBehaviour
{
    #region ���
    [SerializeField] Transform teleportTarget;  //�ǰe�ت��a
    [SerializeField] GameObject player;         //���a
    [SerializeField] GameObject ��������b;      //�ǰe��Ϫ��a���V�����e�� ���઺�ؼЪ�
    [SerializeField] Transform lookAt;          //���a�ݦV���I
    #endregion

    #region ��k
    /// <summary>
    /// ��Ĳ�������
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //�p�G��Ĳ�������Ҭ�"���a"
        if(other.CompareTag("Player"))
        {
            player.transform.position = teleportTarget.position;                                                        //�N���a�ǰe�ܥت��a
            lookAt.position = lookAt.position + new Vector3(0f, player.transform.position.y - lookAt.position.y, 0f);   //�Ϫ��a�ݦV���I������y���󪱮a������y
            ��������b.transform.LookAt(lookAt);                                                                         //�Ϫ��a���V�ݦV���I
        }
    }
    #endregion
}
