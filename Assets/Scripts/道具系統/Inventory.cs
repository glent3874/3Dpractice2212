using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Windows<Inventory>
{
    [SerializeField] GameObject ��l�ҪO = null;
    [SerializeField] RectTransform panal = null;
    [SerializeField] ��l[] slots;

    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();
        UpdateUI();
        PlayerInfoManager.instance.�D��o���ܤ� += UpdateUI;

        slots = panal.GetComponentsInChildren<��l>();
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
        Time.timeScale = 1f - Mathf.Clamp01(Inventory.ins.alpha * 0.9f + PauseMenu.ins.alpha);
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

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            //�ثe����l(�q0) < �s�ɤ����a���W�����P�D��� => ������� 
            if (i < SaveManager.instance.saveData.stuffs.Count)
            {
                //�ǰe�s�ɤ����D����(id,�ƶq)�� ��l
                slots[i].�������(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                slots[i].�M�����();
            }
        }
    }
}