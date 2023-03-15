using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    #region ���
    [SerializeField] GameObject decalPrefab = null;
    [SerializeField] public string saveKey = "";

    public bool havePistol = false;
    #endregion

    #region �ƥ�
    private void Start()
    {
        if (PlayerInfoManager.instance.GetBool(saveKey) == true)
        {
            havePistol = true;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(havePistol)
                Fire();
        }
    }
    #endregion

    #region ��k
    void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f)) 
        {
            Instantiate(decalPrefab, hitInfo.point, Quaternion.FromToRotation(new Vector3(0, 0, 1), hitInfo.normal));
        }
    }
    #endregion
}
