using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �D����UI
/// ����}���PUI����s
/// </summary>
public class Inventory : Windows<Inventory>
{
    #region ���
    [SerializeField] GameObject ��l�ҪO = null;
    [SerializeField] RectTransform panal = null;
    [SerializeField] ��l[] slots;
    #endregion

    #region �ƥ�
    protected override void Start()
    {
        base.Start();
        StuffManager.instance.Load();                                   // �n�D�Ϯ��]��Ū���Ҧ��D���ƨ�t�Τ�
        UpdateUI();                                                     // �}�ҹD�������s�@��
        PlayerInfoManager.instance.�D��o���ܤ� += UpdateUI;             // �e����sUI���ʧ@�쪱�a�D��t��, �s���β�����ƫᰵ��sUI ���ʧ@

        slots = panal.GetComponentsInChildren<��l>();                  // ���o��l
    }

    private void OnDisable()
    {
        PlayerInfoManager.instance.�D��o���ܤ� -= UpdateUI;             // �h�q�e��
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
        Time.timeScale = 1f - Mathf.Clamp01(Inventory.ins.alpha * 0.9f + PauseMenu.ins.alpha);      //�}�ҹD���椣�|�����Ȱ�
    }
    #endregion

    #region ��k
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

    /// <summary>
    /// �d�ߦs�ɨöǰe��Ƶ���l���
    /// </summary>
    void UpdateUI()
    {
        // ���L�C�Ӯ�l
        for (int i = 0; i < slots.Length; i++)
        {
            // �D��s�ɤ����X����ƴN�ǰe��Ƶ���l
            if (i < SaveManager.instance.saveData.stuffs.Count)                 
            {
                //�ǰe�s�ɤ����D����(id,�ƶq)�� ��l
                slots[i].�������(SaveManager.instance.saveData.stuffs[i]);
            }
            else
            {
                // �٦���l���S��ƤF �N�M����l
                slots[i].�M�����();
            }
        }
    }
    #endregion
}