
public class PowerUpWeapon : PowerUp {

  public WeaponType weaponType;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory playerInventory)) {
      playerInventory.AddWeapon(new WeaponType[]{ weaponType });
      base.OnCollect(collector);
    }
  }
}
