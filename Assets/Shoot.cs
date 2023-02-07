using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    #region ���
    [SerializeField] GameObject decalPrefab = null;
    #endregion

    #region �ƥ�
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    #endregion

    #region ��k
    void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo, 100f))
        {
            Instantiate(decalPrefab, hitInfo.point, Quaternion.FromToRotation(new Vector3(0, 0, 1), hitInfo.normal));
        }
    }
    #endregion
}
