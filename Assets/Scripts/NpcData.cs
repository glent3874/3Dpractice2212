using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�s�奻", menuName = "�إ߷s�奻")]
public class NpcData : ScriptableObject
{
    public List<��> ��ܦC��;
}

[System.Serializable] //�Ϩ�ǦC��
public struct ��
{
    public string ����;
    //public bool ����;
    [TextArea(2,3)]
    public string ���e;
}