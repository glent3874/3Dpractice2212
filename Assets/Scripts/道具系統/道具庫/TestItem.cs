using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour, Interactable
{
    [SerializeField] NpcData �奻 = null;

    int ���ʦ��� = 0;

    public void Interact()
    {
        if(���ʦ��� == 0)
        {
            ��ܨt��.instance.�}�l���(�奻, this.transform.position);
        }

        if(���ʦ��� == 1)
        {
            PlayerInfoManager.instance.AddItem(99);
            Destroy(this.gameObject);
        }

        ���ʦ���++;
    }
}
