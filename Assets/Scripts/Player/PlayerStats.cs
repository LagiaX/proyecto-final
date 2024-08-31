using UnityEngine;
using System;

public class PlayerStats : OrganicTarget {
  public delegate void IsPlayerAlive(PlayerStats ps);
  public static event IsPlayerAlive PlayerAlive;
  public delegate void IsPlayerDead(PlayerStats ps);
  public static event IsPlayerDead PlayerDead;

  public delegate void isPlayerHealed(int healing);
  public static event isPlayerHealed PlayerHealed;
  public delegate void isPlayerDamaged(int healing);
  public static event isPlayerDamaged PlayerDamaged;

  public StatsPlayer stats;
  public bool[] ailments;
  public float[] ailmentDuration;
  public float poisonTickTimer = AppConfig.PoisonTickRate;

  protected override void Start() {
    InitStats();
    InitStatusAilments();
    PlayerAlive?.Invoke(this);
  }

  protected override void Update() {
    if (!health.IsAlive()) {
      OnDeath();
      return;
    }
    if (ailments[(int)Ailment.Poisoned]) {
      ApplyPoisonDamage();
    }
  }

  private void ApplyPoisonDamage() {
    // no longer alive OR poison effect finished, reset poison mechanic
    if (!health.IsAlive() || ailmentDuration[(int)Ailment.Poisoned] <= 0) {
      OnCureAilment(Ailment.Poisoned);
      poisonTickTimer = 0f;
      return;
    }

    if (poisonTickTimer <= 0) {
      // change to OnPoisonDamage when poison VFX is ready
      OnDamage(AppConfig.PoisonDamage);
      poisonTickTimer = AppConfig.PoisonTickRate;
    }
    poisonTickTimer -= Time.deltaTime;
    ailmentDuration[(int)Ailment.Poisoned] -= Time.deltaTime;
  }

  public void InitStats() {
    health = Utils.GetPlayerBaseHealth();
    stats = Utils.GetPlayerBaseStats();
  }

  public void InitStatusAilments() {
    ailments = new bool[Enum.GetNames(typeof(Ailment)).Length];
    ailmentDuration = new float[Enum.GetNames(typeof(Ailment)).Length];
    for (int i = 0; i < ailments.Length; i++) {
      ailments[i] = false;
      ailmentDuration[i] = 0f;
    }
  }

  public override void OnDamage(int damage) {
    base.OnDamage(damage);
    PlayerDamaged?.Invoke(damage);
  }

  public void OnRestoreHealth(int healing) {
    health.RestoreHealth(healing);
    PlayerHealed?.Invoke(healing);
  }

  public void OnNewAilment(Ailment a, float duration) {
    ailments[(int)a] = true;
    ailmentDuration[(int)a] = duration;
  }

  public void OnCureAilment(Ailment a) {
    ailments[(int)a] = false;
    ailmentDuration[(int)a] = 0;
  }

  public override void OnDeath() {
    // play death animation
    PlayerDead?.Invoke(this);
  }
}
