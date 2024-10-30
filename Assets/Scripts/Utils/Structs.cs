using System;
using UnityEngine;

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

  public StatsPlayer(string name, int movementSpeedBase, int movementSpeedMax) {
    this.name = name;
    this.movementSpeedBase = movementSpeedBase;
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
  public ManaCircuitInventory[] manaCircuits;

  public const int COINS_MAX = 99;

  public Inventory(int coins, WeaponType[] weapons, ManaCircuitInventory[] manaCircuits) {
    this.coins = coins % COINS_MAX;
    this.weapons = weapons;
    this.manaCircuits = manaCircuits;
  }
}

[Serializable]
public struct ManaCircuitInventory {
  public int id;
  public bool collected;

  public ManaCircuitInventory(int id, bool collected) {
    this.id = id;
    this.collected = collected;
  }
}

public struct PistolStats {
  public const int power = 2;
  public const float shootingRange = 15f;
  public const float shootingSpeed = 15f;
  public const float shootingMoveSpeed = 0.7f;
  public const float fireRate = 1.2f;
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
  public int enemiesDefeated;
  public bool[] puzzleRooms;
  public bool[] enemyRooms;
  public bool[] coins;
  public bool[] circuits;
  public bool[] powerUps;
  public bool[] triggers;
}

[Serializable]
public struct Player {
  public Health health;
  public Inventory inventory;
  public Vector3 position;
  public Vector3 rotation;
}

public struct LevelInfo {
  public int level;
  public int totalPuzzleRooms;
  public int totalEnemyRooms;
  public int totalCoins;
  public int totalCircuits;
  public int totalPowerUps;
  public int totalTriggers;

  public LevelInfo(int level, int puzzleRooms, int enemyRooms, int coins, int circuits, int powerups, int triggers) {
    this.level = level;
    totalPuzzleRooms = puzzleRooms;
    totalEnemyRooms = enemyRooms;
    totalCoins = coins;
    totalCircuits = circuits;
    totalPowerUps = powerups;
    totalTriggers = triggers;
  }
}

public struct CoinInfo {
  public string name;
  public Vector3 position;
  public bool collected;

  public CoinInfo(string name, Vector3 position, bool collected) {
    this.name = name;
    this.position = position;
    this.collected = collected;
  }
}

[Serializable]
public struct PowerupInfo {
  public string name;
  public Powerup type;
  public Vector3 position;
  public bool collected;

  public PowerupInfo(string name, Powerup type, Vector3 position, bool collected) {
    this.name = name;
    this.type = type;
    this.position = position;
    this.collected = collected;
  }
}

[Serializable]
public struct Profile {
  public string name;
}

[Serializable]
public struct SavedConfig {
  public SoundSettings soundSettings;
  public ButtonSettings buttonSettings;
}

[Serializable]
public struct SoundSettings {
  public float bgmVolume;
  public float sfxVolume;
}

[Serializable]
public struct ButtonSettings {
  public int jump;
  public int shoot;
  public int changeWeapon;
  public int lockTarget;
}

public struct Shortcuts {
  public static readonly string Emission = "_EmissionColor";
}
