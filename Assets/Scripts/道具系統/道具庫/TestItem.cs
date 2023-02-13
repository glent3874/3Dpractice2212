using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour, Interactable
{
    [SerializeField] NpcData 文本 = null;

    int 互動次數 = 0;

    public void Interact()
    {
        if(互動次數 == 0)
        {
            對話系統.instance.開始對話(文本, this.transform.position);
        }

        if(互動次數 == 1)
        {
            PlayerInfoManager.instance.AddItem(99);
            Destroy(this.gameObject);
        }

        互動次數++;
    }
}
