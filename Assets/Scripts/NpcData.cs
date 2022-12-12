using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "新文本", menuName = "建立新文本")]
public class NpcData : ScriptableObject
{
    public List<話> 對話列表;
}

[System.Serializable] //使其序列化
public struct 話
{
    public string 講者;
    //public bool 左邊;
    [TextArea(2,3)]
    public string 內容;
}