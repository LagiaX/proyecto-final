using UnityEngine;
using System;
using System.Threading.Tasks;

public class PlayerStats : OrganicTarget {
  public delegate void IsPlayerAlive(PlayerStats ps);
  public static event IsPlayerAlive PlayerAlive;
  public delegate void IsPlayerDead(PlayerStats ps);
  public static event IsPlayerDead PlayerDead;

  public delegate void OnHealthChange(Health h);
  public static event OnHealthChange HealthChanged;
  public delegate void OnPlayerHeal(int healing);
  public static event OnPlayerHeal PlayerHealed;
  public delegate void OnPlayerDamage(int healing);
  public static event OnPlayerDamage PlayerDamaged;

  public StatsPlayer stats;
  public float[] buffDuration;
  public float[] ailmentDuration;

  public AudioSource damageSFX;
  public AudioSource poisonDamageSFX;

  private float poisonTickTimer = AppConfig.PoisonTickRate;
  private float attackMod = 1f;
  private float speedMod = 1f;

  protected override void Start() {
    PlayerAlive?.Invoke(this);
  }

  protected override void Update() {
    if (!health.IsAlive()) {
      OnDeath();
      return;
    }
  }

  private void _SetModifier(Buff b, float value) {
    switch (b) {
      case Buff.Attack:
        attackMod = value;
        break;
      case Buff.Speed:
        speedMod = value;
        break;
    }
  }

  private void _ApplyAilmentDamage(Ailment a) {
    switch (a) {
      case Ailment.Burned:
        OnDamage(AppConfig.BurnDamage);
        break;
      case Ailment.Poisoned:
        OnPoisonDamage(AppConfig.PoisonDamage);
        break;
    }
  }

  private async void _CountBuffDuration(Buff b) {
    while (buffDuration[(int)b] > 0) {
      buffDuration[(int)b] -= Time.deltaTime;
      await Task.Yield();
    }
    _SetModifier(b, 1f);
  }

  private async void _CountAilmentDuration(Ailment a) {
    while (ailmentDuration[(int)a] > 0) {
      if (poisonTickTimer > 0) {
        poisonTickTimer -= Time.deltaTime;
      }
      else {
        // change to OnPoisonDamage when poison VFX is ready
        _ApplyAilmentDamage(a);
        poisonTickTimer = AppConfig.PoisonTickRate;
      }
      ailmentDuration[(int)a] -= Time.deltaTime;
      await Task.Yield();
    }
  }

  public float MovementSpeed() {
    return stats.movementSpeedBase * speedMod;
  }

  public void SetHealth(Health health) {
    this.health = health;
    HealthChanged?.Invoke(health);
  }

  public void InitStats() {
    stats = Utils.GetPlayerBaseStats();
  }

  public void InitBuffs() {
    buffDuration = new float[Enum.GetNames(typeof(Buff)).Length];
  }

  public void InitStatusAilments() {
    ailmentDuration = new float[Enum.GetNames(typeof(Ailment)).Length];
  }

  public override void OnDamage(int damage) {
    base.OnDamage(damage);
    PlayerDamaged?.Invoke(damage);
    damageSFX.Play();
  }

  public override void OnPoisonDamage(int damage) {
    base.OnDamage(damage);
    PlayerDamaged?.Invoke(damage);
    poisonDamageSFX.Play();
  }

  public void OnRestoreHealth(int healing) {
    health.RestoreHealth(healing);
    PlayerHealed?.Invoke(healing);
  }

  public void OnNewBuff(Buff b, float modifier, float duration) {
    if (buffDuration[(int)b] > 0) {
      buffDuration[(int)b] = duration;
      return;
    }
    buffDuration[(int)b] = duration;
    _SetModifier(b, modifier);
    _CountBuffDuration(b);
  }

  public void OnDispellBuff(Buff b) {
    buffDuration[(int)b] = 0;
  }

  public void OnNewAilment(Ailment a, float duration) {
    if (ailmentDuration[(int)a] > 0) {
      ailmentDuration[(int)a] = duration;
      return;
    }
    ailmentDuration[(int)a] = duration;
    _CountAilmentDuration(a);
  }

  public void OnCureAilment(Ailment a) {
    ailmentDuration[(int)a] = 0;
  }

  public override void OnDeath() {
    // play death animation
    PlayerDead?.Invoke(this);
  }
}
