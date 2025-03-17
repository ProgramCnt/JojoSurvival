using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Rock,
    Tree
}

public class Entity : MonoBehaviour, IEntity
{
    public int curHealth;
    public int maxHealth;

    public EntityType entityType;
    public ItemData dropItem;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void OnTakeDamage(int damage)
    {
        curHealth -= damage;
        if (damage > 0)
        {
            DropItem();
        }
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void DropItem()
    {
        Instantiate(dropItem.dropPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
    }
}
