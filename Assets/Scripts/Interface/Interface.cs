using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public interface IState
{
    public void StateEnter();
    public void StateUpdate();
    public void StateExit();
}

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}