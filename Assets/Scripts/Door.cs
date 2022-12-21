using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�������Y���ഫ
/// </summary>
public class Door : MonoBehaviour
{
    #region ���
    [SerializeField] GameObject cinamachine = null;     //��v��
    #endregion

    #region ��k
    /// <summary>
    /// �}�l���Y�ഫ
    /// </summary>
    public void StartAnimation()
    {
        cinamachine.SetActive(true); 
    }

    /// <summary>
    /// �������Y�ഫ
    /// </summary>
    public void EndAnimation()
    {
        Destroy(cinamachine);
        Destroy(this.gameObject);
    }
    #endregion
}
