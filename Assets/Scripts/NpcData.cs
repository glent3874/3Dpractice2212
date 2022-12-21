using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 新增創建文本選項
/// 建立文本列表
/// </summary>
[CreateAssetMenu(fileName = "新文本", menuName = "建立新文本")]
public class NpcData : ScriptableObject
{
    public List<話> 對話列表;
}

[System.Serializable] //使其序列化
public struct 話
{
    public string 講者;
    [TextArea(2,3)]
    public string 內容;
}