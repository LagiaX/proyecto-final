using UnityEngine;
using System;

public class PlayerStats : AliveTarget {
  public delegate void IsAlive();
  public static event IsAlive Alive;

  public CharacterStats stats;
  public bool[] ailments;
  public float[] ailmentTimers;

  void OnEnable() {
    Alive.Invoke();
  }

  void Start() {
    InitializeStats();
  }

  void Update() {
    CheckAilmentPoison();
  }

  private void CheckAilmentPoison() {
    if (ailments[(int)Ailment.Poisoned]) {
      // change to OnPoisonDamage when poison material is ready
      base.OnDamage(1);
      ailmentTimers[(int)Ailment.Poisoned] -= Time.deltaTime;
    }
  }

  public override void OnDamage(int damage) {
    base.OnDamage(damage);
  }

  public void OnNewAilment(Ailment a, float duration) {
    ailments[(int)a] = true;
    ailmentTimers[(int)a] = duration;
  }

  public void OnCureAilment(Ailment a) {
    ailments[(int)a] = false;
    ailmentTimers[(int)a] = 0;
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

  public void InitializeStats() {
    health = Utils.GetCharacterBaseHealth();
    stats = Utils.GetCharacterBaseStats();
    ailments = new bool[Enum.GetNames(typeof(Ailment)).Length];
    ailmentTimers = new float[Enum.GetNames(typeof(Ailment)).Length];
  }
}
