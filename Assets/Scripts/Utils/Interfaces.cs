using UnityEngine;

public interface ICollectable {
  public void OnCollect(OrganicTarget collector);
}

public interface IDamageable {
  public void OnDamage(int damage);
  public void OnDeath();
}

public interface IDestructible {
  public void OnDestruct();
}

public interface IInteractable {
  public void OnInteract();
}

public interface ILiftable {
  public void OnLift();
  public void OnPutDown();
  public void OnThrow(float speed);
}

public interface IShootable {
  public void OnShoot(float delay);
  public void OnImpact(float speed);
}

public interface ITargetable {
  public void OnFocusEnter(Vector3 position);
  public void OnFocusExit();
}

public interface IWieldable {
  public void OnWield();
  public void OnAttack();
  public void OnCastOff();
}