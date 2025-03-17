using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Rock,
    Tree
}

public class Entity : MonoBehaviour
{
    public int capacity;

    public EntityType entityType;
    public ItemData dropItem;

    public void OnTakeDamage(RaycastHit hit)
    {
        capacity--;
        if (hit.point != null && hit.normal != null)
        {
            DropItem(hit);
        }
        if (capacity <= 0)
        {
            Destroy(gameObject);
        }
    }

    void DropItem(RaycastHit hit)
    {
        Instantiate(dropItem.dropPrefab, hit.point + Vector3.up, Quaternion.LookRotation(hit.normal, Vector3.up));
    }
}
