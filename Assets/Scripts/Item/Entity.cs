using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    public int curHealth;
    public int maxHealth;

    public ItemData dropItem;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    public void OnTakeDamage(int damage)
    {
        Instantiate(dropItem.dropPrefab, transform.position, Quaternion.identity);
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
