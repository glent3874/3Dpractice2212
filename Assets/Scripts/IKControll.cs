using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControll : MonoBehaviour
{
    [SerializeField] Animator kyleAnimator = null;
    public Vector3 lookAt = Vector3.zero;

    //允許寫入IK數據的窗口期
    private void OnAnimatorIK(int layerIndex)
    {
        kyleAnimator.SetLookAtPosition(lookAt);
        kyleAnimator.SetLookAtWeight(1f);
    }
}
