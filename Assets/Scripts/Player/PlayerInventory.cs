using UnityEngine;

public class PlayerInventory : MonoBehaviour {

  public delegate void OnCollectWeapon(WeaponType type, int slot);
  public static event OnCollectWeapon WeaponCollect;

  public delegate void OnChangeWeapon(int slot);
  public static event OnChangeWeapon WeaponChange;

  public Inventory inventory;

  public RangedWeapon pistolPrefab;
  public RangedWeapon crossbowPrefab;
  public RangedWeapon shotgunPrefab;

  protected int _weaponsIndex = 0;

  void Start() {
    // TODO: Initialize with savefile contents
    inventory = new Inventory(0, new WeaponType[2] { WeaponType.Unarmed, WeaponType.Unarmed });
  }

  private void _HideShowWeapons(WeaponType activeWeapon) {
    RemoveWeapon();
    RangedWeapon prefab = null;
    switch (activeWeapon) {
      case WeaponType.Pistol:
        prefab = pistolPrefab;
        break;
      case WeaponType.Crossbow:
        prefab = crossbowPrefab;
        break;
      case WeaponType.Shotgun:
        prefab = shotgunPrefab;
        break;
      default:
        prefab = null;
        break;
    }
    if (prefab != null) {
      PlayerSystems.instance.actions.weapon = Instantiate(prefab, PlayerSystems.instance.weaponSlot.transform);
    }
  }

  private int _NextIndexWrap() {
    return (_weaponsIndex + 1) % inventory.weapons.Length;
  }

  public void AddCoinValue(int value) {
    inventory.coins = (inventory.coins + value) % Inventory.COINS_MAX;
    HeadUpDisplay.instance.ModifyCoins(inventory.coins);
  }

  public void AddWeapon(WeaponType type) {
    if (inventory.weapons[_weaponsIndex] == WeaponType.Unarmed || (inventory.weapons[_NextIndexWrap()] != WeaponType.Unarmed)) {
      inventory.weapons[_weaponsIndex] = type;
      _HideShowWeapons(type);
      WeaponCollect?.Invoke(type, _weaponsIndex);
    }
    else {
      inventory.weapons[_NextIndexWrap()] = type;
      WeaponCollect?.Invoke(type, _NextIndexWrap());
    }
  }

  public void ChangeWeapon() {
    _weaponsIndex = _NextIndexWrap();
    _HideShowWeapons(inventory.weapons[_weaponsIndex]);
    WeaponChange?.Invoke(_weaponsIndex);
  }

  public async void RemoveWeapon() {
    if (PlayerSystems.instance.actions.weapon != null)
      await GarbageManager.RemoveInTime(PlayerSystems.instance.actions.weapon.gameObject, 1);
  }
}
