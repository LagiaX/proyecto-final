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

  public void ModifyMissingHealth(int value) {
    healthMissing = Mathf.Clamp(healthMissing + value, 0, healthMax);
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
public struct CharacterStats {
  public string name;
  public int movementSpeedBase;
  public float movementSpeedModifier;
  public int movementSpeed {
    get {
      return (int)(movementSpeedBase * movementSpeedModifier);
    }
  }
  public int movementSpeedMax;
  const float movementSpeedModMax = 2.8f;

  public CharacterStats(string name, int movementSpeed, float movementSpeedModifier, int movementSpeedMax) {
    this.name = name;
    this.movementSpeedBase = movementSpeed;
    this.movementSpeedModifier = movementSpeedModifier;
    this.movementSpeedMax = movementSpeedMax;
  }

  public void ModifyMovementSpeed(float modifier) {
    movementSpeedModifier = Mathf.Clamp(movementSpeedModifier * modifier, 0.5f, movementSpeedModMax);
  }
}

public struct NonPlayableCharacterStats {
  public string name;
  public int attack;
  public int attackMax;
  public int actionSpeed;
  public int movementSpeed;
  public int movementSpeedMax;

  public NonPlayableCharacterStats(string name, int attack, int attackMax, int actionSpeed, int actionSpeedMax, int movementSpeed, int movementSpeedMax) {
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

public struct Inventory {
  public int coins;
  public WeaponType[] weapons;

  public const int COINS_MAX = 99;

  public Inventory(int coins, WeaponType[] weapons) {
    this.coins = coins % COINS_MAX;
    this.weapons = weapons;
  }
}

public struct GunStats {
  public const int power = 1;
  public const float shootingRange = 10f;
  public const float shootingSpeed = 15f;
  public const float shootingMoveSpeed = 0.7f;
  public const float fireRate = 1f;
  public const bool instantFire = true;
}

public struct CrossbowStats {
  public const int power = 3;
  public const float shootingRange = 8f;
  public const float shootingSpeed = 18f;
  public const float shootingMoveSpeed = 0.9f;
  public const float fireRate = 1.5f;
  public const bool instantFire = true;
}

public struct ShotgunStats {
  public const int power = 5;
  public const float shootingRange = 4f;
  public const float shootingSpeed = 25f;
  public const float shootingMoveSpeed = 0.5f;
  public const float fireRate = 0.7f;
  public const bool instantFire = true;
}

public enum WeaponType {
  Gun,
  Crossbow,
  Shotgun,
  Unarmed
}

public enum ProyectileType {
  Bullet,
  Bolt,
  Pellets
}

public struct SaveFile {
  public Systems systems;
  public Player player;
}

public struct Systems {
  public int scene;
  public int savePoint;
}

public struct Player {
  public Profile profile;
  public CharacterStats stats;
  public Inventory inventory;
  public Vector3 position;
  public Vector3 rotation;
}

public struct Profile {
  public string name;
}