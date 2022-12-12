using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 物件描述 : MonoBehaviour, Item
{
    [SerializeField] NpcData 描述文本 = null;
    public void interact()
    {
        對話系統.instance.開始對話(描述文本, this.transform.position);
    }
}
