
using System;

public class PlayerStats : AliveTarget {
  public delegate void IsAlive();
  public static event IsAlive Alive;

  public CharacterStats stats;

  void OnEnable() {
    Alive.Invoke();
  }

  void Start() {
    health = Utils.GetCharacterBaseHealth();
    stats = Utils.GetCharacterBaseStats();
  }

  public override void OnDamage(int damage) {
    base.OnDamage(damage);
  }

  public override void OnDeath() {
    // disable inputs
    if (gameObject.TryGetComponent(out PlayerActions controls)) {
      controls.enabled = false;
    }
    // play death animation
    // respawn after 3 secs
    Utils.DelayFor(() => SpawnManager.instance.Respawn(), TimeSpan.FromSeconds(3));
  }
}
