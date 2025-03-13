using System;
using UnityEngine;

public interface IDamageable
{
 void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamageable
{
 public UICondition uiCondition;

 Condition health { get { return uiCondition.health; } }
 Condition hunger { get { return uiCondition.hunger; } }
 Condition stamina { get { return uiCondition.stamina; } }

 public float noHungerHealthDecay;
 public event Action onTakeDamage;

 private void Update()
 {
  hunger.Subtract(hunger.passiveValue * Time.deltaTime);
  stamina.Add(stamina.passiveValue * Time.deltaTime);

  if(hunger.curValue == 0f)
  {
   health.Subtract(noHungerHealthDecay * Time.deltaTime);
  }

  if(health.curValue == 0f)
  {
   Die();
  }
 }

 public void Heal(float amount)
 {
  health.Add(amount);
 }

 public void Eat(float amount)
 {
  hunger.Add(amount);
 }

 public void Die()
 {
  Debug.Log("플레이어가 죽었다.");
 }
//데미지_캠프파이어*
 public void TakePhysicalDamage(int damage)
 {
  health.Subtract(damage);
  onTakeDamage?.Invoke();
 }
}
