using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[Serializable]
public struct Health {
  public int healthMax;
  public int healthMissing;
  [SerializeField]
  public int healthCurrent {
    get {
      return healthMax - healthMissing;
    }
  }

  public Health(int healthMax, int healthMissing) {
    this.healthMax = healthMax;
    this.healthMissing = healthMissing;
  }

  public void RestoreHealth(int value) {
    healthMissing = Mathf.Clamp(healthMissing - Mathf.Abs(value), 0, healthMax);
  }

  public void LoseHealth(int value) {
    healthMissing = Mathf.Clamp(healthMissing + Mathf.Abs(value), 0, healthMax);
  }

  public bool IsAlive() {
    return healthCurrent > 0;
  }
}

public struct Durability {
  public int durability;
  public void ModifyDurability() {
    durability--;
  }
}

[Serializable]
public struct StatsPlayer {
  public string name;
  public int movementSpeedBase;
  public int movementSpeedMax;

  public StatsPlayer(string name, int movementSpeed, int movementSpeedMax) {
    this.name = name;
    this.movementSpeedBase = movementSpeed;
    this.movementSpeedMax = movementSpeedMax;
  }
}

public struct StatsNpc {
  public string name;
  public int attack;
  public int attackMax;
  public int actionSpeed;
  public int movementSpeed;
  public int movementSpeedMax;

  public StatsNpc(string name, int attack, int attackMax, int actionSpeed, int actionSpeedMax, int movementSpeed, int movementSpeedMax) {
    this.name = name;
    this.attack = attack;
    this.attackMax = attackMax;
    this.actionSpeed = actionSpeed;
    this.movementSpeed = movementSpeed;
    this.movementSpeedMax = movementSpeedMax;
  }

  public void ModifyAttack(float modifier) {
    attack = (int)Mathf.Clamp(attack * modifier, 0, attackMax);
  }

  public void ModifyActionSpeed(float modifier) {
    actionSpeed = (int)Mathf.Clamp(actionSpeed * modifier, 0, 2);
  }

  public void ModifyMovementSpeed(float modifier) {
    movementSpeed = (int)Mathf.Clamp(movementSpeed * modifier, 0, movementSpeedMax);
  }
}

[Serializable]
public struct Inventory {
  public int coins;
  public WeaponType[] weapons;

  public const int COINS_MAX = 99;

  public Inventory(int coins, WeaponType[] weapons) {
    this.coins = coins % COINS_MAX;
    this.weapons = weapons;
  }
}

public struct PistolStats {
  public const int power = 1;
  public const float shootingRange = 15f;
  public const float shootingSpeed = 15f;
  public const float shootingMoveSpeed = 0.7f;
  public const float fireRate = 1f;
}

public struct CrossbowStats {
  public const int power = 3;
  public const float shootingRange = 8f;
  public const float shootingSpeed = 20f;
  public const float shootingMoveSpeed = 0.9f;
  public const float fireRate = 1.5f;
}

public struct ShotgunStats {
  public const int power = 5;
  public const float shootingRange = 4f;
  public const float shootingSpeed = 25f;
  public const float shootingMoveSpeed = 0.5f;
  public const float fireRate = 0.7f;
}

// SAVEFILE & CONFIG

[Serializable]
public struct SaveFile {
  public Systems systems;
  public Player player;
}

[Serializable]
public struct Systems {
  public string scene;
  public int savePoint;
}

[Serializable]
public struct Player {
  public Profile profile;
  public StatsPlayer stats;
  public Inventory inventory;
  public Vector3 position;
  public Vector3 rotation;
}

[Serializable]
public struct Profile {
  public string name;
}

[Serializable]
public struct SavedConfig {
  public SoundSettings soundSettings;
}

[Serializable]
public struct SoundSettings {
  public float bgmVolume;
  public float sfxVolume;
  public AudioSpeakerMode speakerMode;
}

public struct Shortcuts {
  public static readonly string Emission = "_EmissionColor";
}
