using System.Collections;
using UnityEngine;

public class NPCCombat : MonoBehaviour, IDamageable
{
    [SerializeField] private ItemData[] _dropOnDeath;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _attackValue;
    [SerializeField] private int _npcMaxHP;
    private int _curHP;

    private SkinnedMeshRenderer[] _meshRenderers;
    private PlayerCondition _playerCondition;

    private void Start()
    {
        _curHP = _npcMaxHP;
        if(CharacterManager.Instance != null)
        _playerCondition = CharacterManager.Instance.Player.condition;
    }

    public void TakePhysicalDamage(int _damage)
    {
        _curHP -= _damage;

        if(_curHP <= 0)
        {
            Die();
        }

        StartCoroutine(DamageFlash());
    }

    void Die()
    {
        foreach(ItemData item in _dropOnDeath)
        {
            Instantiate(item.dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        for(int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material.color = Color.white;
        }
    }

    public void Attack()
    {
        if(Physics.Raycast(transform.position, Vector3.forward, _attackRange, _layerMask))
        {
            _playerCondition.TakePhysicalDamage(_attackValue);
        }
    }
}
