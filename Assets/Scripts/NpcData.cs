using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �s�W�Ыؤ奻�ﶵ
/// �إߤ奻�C��
/// </summary>
[CreateAssetMenu(fileName = "�s�奻", menuName = "�إ߷s�奻")]
public class NpcData : ScriptableObject
{
    public List<��> ��ܦC��;
}

[System.Serializable] //�Ϩ�ǦC��
public struct ��
{
    public string ����;
    [TextArea(2,3)]
    public string ���e;
}