using UnityEngine;

public class PlayerInventory : MonoBehaviour {

  public Inventory inventory;
  public GameObject weaponSlot;
  protected int _weaponsIndex = -1;

  public void Awake() {
    weaponSlot = transform.Find("WeaponSlot").gameObject;
  }

  public void Start() {
    // TODO: Initialize with savefile contents
    inventory = new Inventory(0, new WeaponType[2] { WeaponType.Unarmed, WeaponType.Unarmed });
  }

  private void _HideShowWeapons(WeaponType activeWeapon) {
    print(activeWeapon);
    switch (activeWeapon) {
      case WeaponType.Gun:
        //weaponSlot.transform.Find("Crossbow_Equipment").gameObject.SetActive(false);
        //weaponSlot.transform.Find("Shotgun_Equipment").gameObject.SetActive(false);
        RangedWeapon r = weaponSlot.transform.Find("Gun_Equipment").gameObject.GetComponent<RangedWeapon>();
        r.gameObject.SetActive(true);
        PlayerSystems.instance.actions.weaponPrefab = r;
        break;
      case WeaponType.Crossbow:
        weaponSlot.transform.Find("Gun_Equipment").gameObject.SetActive(false);
        weaponSlot.transform.Find("Shotgun_Equipment").gameObject.SetActive(false);
        weaponSlot.transform.Find("Crossbow_Equipment").gameObject.SetActive(true);
        break;
      case WeaponType.Shotgun:
        weaponSlot.transform.Find("Gun_Equipment").gameObject.SetActive(false);
        weaponSlot.transform.Find("Crossbow_Equipment").gameObject.SetActive(false);
        weaponSlot.transform.Find("Shotgun_Equipment").gameObject.SetActive(true);
        break;
    }
  }

  public void AddCoinValue(int value) {
    inventory.coins = (inventory.coins + value) % Inventory.COINS_MAX;
    HeadUpDisplay.instance.ModifyCoins(inventory.coins);
  }

  public void AddWeapon(WeaponType type) {
    if (_weaponsIndex == -1) {
      _weaponsIndex = 0;
    }
    inventory.weapons[_weaponsIndex] = type;
    _HideShowWeapons(type);
  }

  public void SwitchWeapon() {
    if (inventory.weapons.Length < 2) return;
    _weaponsIndex = (_weaponsIndex + 1) % inventory.weapons.Length;
    _HideShowWeapons(inventory.weapons[_weaponsIndex]);
  }
}
