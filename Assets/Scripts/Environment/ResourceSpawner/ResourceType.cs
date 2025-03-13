using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceType
{
    public string name;
    public List<GameObject> prefabs; // ���� ������ �������� ����Ʈ�� ����
    public int maxCount;
    public int respawnThreshold;
    public float minSpacing;
    public float spawnChance = 1f;
}