using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWeapon : PowerUp {

  public WeaponType weaponType;

  public override void OnCollect(AliveTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory playerInventory)) {
      playerInventory.AddWeapon(weaponType);
      base.OnCollect(collector);
    }

  }
}
