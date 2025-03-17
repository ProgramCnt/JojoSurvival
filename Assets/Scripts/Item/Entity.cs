using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Rock,
    Tree
}

public class Entity : MonoBehaviour, IEntity
{
    public int curHealth;
    public int maxHealth;

    public ResourceType entityType;
    public ItemData dropItem;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void OnTakeDamage(int damage)
    {
        if (ResourceType.Rock == entityType)
        {
            DropItem();
            curHealth -= damage;
            if (curHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void DropItem()
    {
        Instantiate(dropItem.dropPrefab, transform.position, Quaternion.identity);
    }
}
