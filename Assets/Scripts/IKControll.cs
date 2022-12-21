using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制人物視線
/// </summary>
public class IKControll : MonoBehaviour
{
    #region 欄位
    [SerializeField] Animator kyleAnimator = null;  //人物動畫控制器
    public Vector3 lookAt = Vector3.zero;           //看向的位置
    #endregion

    #region 方法
    /// <summary>
    /// 寫入IK數據
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        kyleAnimator.SetLookAtPosition(lookAt); //看向的位置
        kyleAnimator.SetLookAtWeight(1f);       //權重
    }
    #endregion
}
