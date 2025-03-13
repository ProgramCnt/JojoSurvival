using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceType
{
    public string name;
    public List<GameObject> prefabs; // 여러 종류의 프리팹을 리스트로 저장
    public int maxCount;
    public int respawnThreshold;
    public float minSpacing;
    public float spawnChance = 1f;
}