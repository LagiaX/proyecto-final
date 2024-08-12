using UnityEngine;

public abstract class AliveTarget : Target, IDamageable {

  public delegate void IsDead(AliveTarget e);
  public static event IsDead Dead;

  public int showDamagedF = 5;
  public Health health;

  protected Material material;
  protected Color color;
  protected int _damagedFCount;

  void Awake() {
    if (!TryGetComponent(out Renderer r)) {
      Utils.MissingComponent(typeof(Renderer).Name, this.GetType().Name);
      return;
    }
    material = r.material;
    color = material.color;
  }

  void Update() {
    // TODO: Check whether this can be done with a coroutine
    _ChangeColorForATime();
  }

  private void _ChangeColorForATime() {
    if (material.color != color) {
      if (_damagedFCount > 0) {
        _damagedFCount--;
        return;
      }
      material.color = color;
    }
  }

  private void _Die() {
    // play dead animation
    Destroy(gameObject);
  }

  public virtual void OnDamage(int damage) {
    _damagedFCount = showDamagedF;
    material.color = color * 1.5f;
    health.LoseHealth(damage);
    if (!health.IsAlive())
      OnDeath();
  }

  public virtual void OnPoisonDamage(int damage) {
    health.LoseHealth(damage);
    if (!health.IsAlive())
      OnDeath();
  }

  public virtual void OnDeath() {
    Dead?.Invoke(this);
    _Die();
  }
}
