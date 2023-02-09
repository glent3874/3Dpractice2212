using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物件描述
/// </summary>
public class 物件描述 : MonoBehaviour, Item
{
    [SerializeField] NpcData 描述文本 = null;
    public void Interact()
    {
        //傳送要使用的文本及此物件的位置
        對話系統.instance.開始對話(描述文本, this.transform.position);
    }
}
