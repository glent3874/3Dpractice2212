using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����H�����u
/// </summary>
public class IKControll : MonoBehaviour
{
    #region ���
    [SerializeField] Animator kyleAnimator = null;  //�H���ʵe���
    public Vector3 lookAt = Vector3.zero;           //�ݦV����m
    #endregion

    #region ��k
    /// <summary>
    /// �g�JIK�ƾ�
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        kyleAnimator.SetLookAtPosition(lookAt); //�ݦV����m
        kyleAnimator.SetLookAtWeight(1f);       //�v��
    }
    #endregion
}
