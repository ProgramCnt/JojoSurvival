using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    private Condition _health;
    public Condition health { get { return _health; } }

    public ItemData dropItem;

    // Start is called before the first frame update
    void Start()
    {
        _health.curValue = _health.maxValue;
    }

    public void OnTakeDamage(int damage)
    {
        Instantiate(dropItem.dropPrefab, transform.position, Quaternion.identity);
        _health.Subtract(damage);
        if (health.curValue <= 0)
        {
            Destroy(gameObject);
        }
    }
}
