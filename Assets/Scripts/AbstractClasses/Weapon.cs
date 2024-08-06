using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWieldable {
  [Header("Attributes")]
  public int power;
  
  public abstract void OnWield();

  public virtual void OnAttack() {
    print("Attacking with " + power + " power.");
  }

  public abstract void OnCastOff();
}