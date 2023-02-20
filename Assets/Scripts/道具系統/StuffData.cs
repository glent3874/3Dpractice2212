using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建立道具檔案
/// </summary>
[CreateAssetMenu(fileName ="New Item", menuName ="Create New Item")]
public class StuffData : ScriptableObject
{
    public int 道具編號;
    public string 道具名稱;
    public string 道具敘述;
    public Sprite 道具圖片;
}
