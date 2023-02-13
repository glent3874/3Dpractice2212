using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : Windows<Inventary>
{
    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();
        UpdateUI();
        PlayerInfoManager.instance.�D��o���ܤ� += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerInfoManager.instance.�D��o���ܤ� -= UpdateUI;
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen == true)
                Close();
            else
                Open();
        }
        Time.timeScale = 1f - Mathf.Clamp01(Inventary.ins.alpha * 0.9f + PauseMenu.ins.alpha);
    }

    public override void Open()
    {
        base.Open();
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Close()
    {
        base.Close();
        Cursor.lockState = CursorLockMode.Locked;
    }

    [SerializeField] GameObject ��l�ҪO = null;
    [SerializeField] GameObject Panal = null;
    [SerializeField] RectTransform �I���� = null;

    void UpdateUI()
    {
        ��l�ҪO.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            //���ͷs��l�å�bPanal�� Panal�|�ۦ�ƦC
            GameObject �s��l = Instantiate(��l�ҪO, �I����);
            �s��l.SetActive(true);

            //�ثe����l(�q0) < �s�ɤ����a���W�����P�D��� => ������� 
            if (i < SaveManager.instance.saveData.stuffs.Count)
            {
                �s��l.GetComponent<��l>().�������(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                //���l�ҪO�̪��Ϥ��P�ƶq�Y��0
                for (int j = 0; j < �s��l.transform.childCount; j++)
                {
                    �s��l.transform.GetChild(j).localScale = Vector3.zero;
                }
            }
        }
    }
}
