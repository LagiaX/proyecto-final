
public class PowerUpWeapon : PowerUp {

  public WeaponType weaponType;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory playerInventory)) {
      playerInventory.AddWeapon(weaponType);
      base.OnCollect(collector);
    }

  }
}
