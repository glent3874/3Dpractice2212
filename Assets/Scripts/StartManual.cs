using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �C���}�l���ާ@����
/// </summary>
public class StartManual : MonoBehaviour
{
    #region ���
    [SerializeField] GameObject �}�l���� = null;
    [SerializeField] NpcData �}�l�奻 = null;
    [SerializeField] Text ���D = null;
    [SerializeField] Text ���� = null;
    [SerializeField] GameObject �~�򴣥� = null;

    public bool isPlay = false;
    #endregion

    #region �ƥ�
    private void Start()
    {
        �}�l���(�}�l�奻);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && stop)
        {
            stop = false;
        }
    }
    #endregion

    #region ��k
    public void �}�l���(NpcData �奻)
    {
        if (isPlay) return;
        StartCoroutine(������(�奻));
    }
    IEnumerator ������(NpcData �奻)
    {
        isPlay = true;
        ����.text = "";
        ���D.text = "";
        stop = false;

        for (int i = 0; i < �奻.��ܦC��.Count; i++)
        {
            ���D.text = �奻.��ܦC��[i].����;

            string ��X = "";

            for (int j = 0; j < �奻.��ܦC��[i].���e.Length; j++) 
            {
                char �s���r = �奻.��ܦC��[i].���e[j];
                ��X = ��X + �s���r;

                ����.text = ��X;
                yield return new WaitForSeconds(0.05f);
            }
            stop = true;
            while (stop)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        isPlay = false;
        �}�l����.SetActive(false);
    }

    bool stop
    {
        get { return _stop; }
        set
        {
            _stop = value;
            if (_stop)
            {
                �~�򴣥�.SetActive(true);
            }
            else
            {
                �~�򴣥�.SetActive(false);
            }
        }
    }
    bool _stop = false;
    #endregion
}
