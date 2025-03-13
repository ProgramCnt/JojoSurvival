using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<ResourceType> resourceTypes;
    private float checkInterval = 10f;
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    public Transform player;
    private float playerAvoidRadius = 5f;

    private Dictionary<ResourceType, List<GameObject>> spawnedResources = new Dictionary<ResourceType, List<GameObject>>();

    void Start()
    {
        foreach (ResourceType resource in resourceTypes)
        {
            spawnedResources[resource] = new List<GameObject>();
            SpawnResources(resource, resource.maxCount);
        }
        InvokeRepeating(nameof(CheckResources), checkInterval, checkInterval);
    }

    void SpawnResources(ResourceType resourceType, int amount)
    {
        int spawnedCount = 0;
        int maxAttempts = amount * 10;

        while (spawnedCount < amount && maxAttempts > 0)
        {
            maxAttempts--;

            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                50f,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            ) + transform.position;

            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, 100f))
            {
                Vector3 spawnPosition = hit.point;

                if (Vector3.Distance(spawnPosition, player.position) < playerAvoidRadius)
                {
                    continue;
                }

                if (Random.value > resourceType.spawnChance)
                {
                    continue;
                }

                //일정 높이에는 생성 안되게끔 처리해야될수도 있음

                if (IsPositionValid(spawnPosition, resourceType.minSpacing))
                {
                    // 나무 종류를 랜덤 선택
                    GameObject randomPrefab = resourceType.prefabs[Random.Range(0, resourceType.prefabs.Count)];

                    GameObject newResource = Instantiate(randomPrefab, spawnPosition, Quaternion.identity, transform);
                    spawnedResources[resourceType].Add(newResource);
                    spawnedCount++;
                }
            }
        }
    }

    bool IsPositionValid(Vector3 position, float minSpacing)
    {
        foreach (var kvp in spawnedResources)
        {
            foreach (GameObject resource in kvp.Value)
            {
                if (resource != null && Vector3.Distance(resource.transform.position, position) < minSpacing)
                {
                    return false;
                }
            }
        }

        Collider[] colliders = Physics.OverlapSphere(position, minSpacing);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Resource"))
            {
                return false;
            }
        }

        return true;
    }

    void CheckResources()
    {
        foreach (ResourceType resourceType in resourceTypes)
        {
            spawnedResources[resourceType].RemoveAll(resource => resource == null);
            int currentCount = spawnedResources[resourceType].Count;

            if (currentCount <= resourceType.respawnThreshold)
            {
                int amountToSpawn = resourceType.maxCount - currentCount;
                SpawnResources(resourceType, amountToSpawn);
            }
        }
    }
}