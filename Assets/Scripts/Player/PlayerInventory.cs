using UnityEngine;

public class PlayerInventory : MonoBehaviour {
  public delegate void OnInventoryChange(Inventory i);
  public static event OnInventoryChange InventoryChanged;
  public delegate void OnCollectCoin(int value);
  public static event OnCollectCoin CoinCollect;
  public delegate void OnCollectCircuit(bool obtained, int id);
  public static event OnCollectCircuit CircuitCollect;
  public delegate void OnCollectWeapon(WeaponType type, int slot);
  public static event OnCollectWeapon WeaponCollect;

  public delegate void OnChangeWeapon(int slot);
  public static event OnChangeWeapon WeaponChange;

  public Inventory inventory;

  public RangedWeapon pistolPrefab;
  public RangedWeapon crossbowPrefab;
  public RangedWeapon shotgunPrefab;

  protected int _weaponsIndex = 0;

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

  public void SetInventory(Inventory inventory) {
    this.inventory = inventory;
    InventoryChanged?.Invoke(inventory);
  }
  public void AddCoinValue(int value) {
    inventory.coins = (inventory.coins + value) % Inventory.COINS_MAX;
    CoinCollect?.Invoke(inventory.coins);
  }
  // TODO: Param should be ManaCircuitInventory[] instead of int[]
  public void AddManaCircuit(int[] ids) {
    for (int i = 0; i < ids.Length; i++) {
      inventory.manaCircuits[ids[i]].collected = true;
      CircuitCollect?.Invoke(inventory.manaCircuits[ids[i]].collected, ids[i]);
    }
  }

  public void AddWeapon(WeaponType[] weapons) {
    for (int i = 0; i < weapons.Length; i++) {
      if (inventory.weapons[_weaponsIndex] == WeaponType.Unarmed || (inventory.weapons[_NextIndexWrap()] != WeaponType.Unarmed)) {
        inventory.weapons[_weaponsIndex] = weapons[i];
        _HideShowWeapons(weapons[i]);
        WeaponCollect?.Invoke(weapons[i], _weaponsIndex);
      }
      else {
        inventory.weapons[_NextIndexWrap()] = weapons[i];
        WeaponCollect?.Invoke(weapons[i], _NextIndexWrap());
      }
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
