using System.Threading.Tasks;
using UnityEngine;

public abstract class OrganicTarget : Target, IDamageable {

  public delegate void IsAlive(OrganicTarget t);
  public static event IsAlive Alive;
  public delegate void IsDead(OrganicTarget t);
  public static event IsDead Dead;

  public Health health;

  private new Renderer renderer;
  private Color matColor;
  private Color dmgColor = Color.white;

  protected virtual void Awake() {
    if (!TryGetComponent(out Renderer r)) {
      Utils.MissingComponent(typeof(Renderer).Name, this.GetType().Name);
      return;
    }
    renderer = r;
    matColor = renderer.material.color;
  }

  protected virtual void Start() {
    Alive?.Invoke(this);
  }

  protected virtual void Update() {
    if (!health.IsAlive())
      OnDeath();
  }

  private async void _ChangeColorForATime() {
    renderer.material.color = dmgColor;
    await Task.Delay(100);
    renderer.material.color = matColor;
  }

  public virtual void OnDamage(int damage) {
    if (health.IsAlive()) {
      _ChangeColorForATime();
      health.LoseHealth(damage);
    }
  }

  public virtual void OnPoisonDamage(int damage) {
    if (health.IsAlive()) {
      // activate VFX for poison
      health.LoseHealth(damage);
    }
  }

  public virtual void OnDeath() {
    Dead?.Invoke(this);
  }
}
