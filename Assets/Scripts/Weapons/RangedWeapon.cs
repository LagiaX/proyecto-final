using UnityEngine;

public class RangedWeapon : Weapon {

  public WeaponType type;
  public float bulletDelay;

  [Header("Ammo")]
  public Proyectile proyectilePrefab;
  public Transform shootingPoint;

  protected float _fireDelay;
  protected float _timer = 0;

  public void Start() {
    _assignWeaponStats();
  }

  void Update() {
    _CalculateFiringRate();
  }

  private void _assignWeaponStats() {
    switch (type) {
      case WeaponType.Gun:
        proyectilePrefab.power = GunStats.power;
        proyectilePrefab.speed = GunStats.shootingSpeed;
        proyectilePrefab.weaponRange = GunStats.shootingRange;
        proyectilePrefab.isFired = GunStats.instantFire;
        _fireDelay = 1 / GunStats.fireRate;
        break;
      case WeaponType.Crossbow:
        proyectilePrefab.power = CrossbowStats.power;
        proyectilePrefab.speed = CrossbowStats.shootingSpeed;
        proyectilePrefab.weaponRange = CrossbowStats.shootingRange;
        proyectilePrefab.isFired = CrossbowStats.instantFire;
        _fireDelay = 1 / CrossbowStats.fireRate;
        break;
      case WeaponType.Shotgun:
        proyectilePrefab.power = ShotgunStats.power;
        proyectilePrefab.speed = ShotgunStats.shootingSpeed;
        proyectilePrefab.weaponRange = ShotgunStats.shootingRange;
        proyectilePrefab.isFired = ShotgunStats.instantFire;
        _fireDelay = 1 / ShotgunStats.fireRate;
        break;
    }
  }

  private void _CalculateFiringRate() {
    if (CanShoot())
      return;
    _timer -= Time.deltaTime;
  }

  public bool CanShoot() {
    return _timer <= 0;
  }

  public override void OnWield() {
    // play animation and sound
  }

  public override void OnAttack() {
    if (CanShoot()) {
      Instantiate(proyectilePrefab, shootingPoint.position, shootingPoint.rotation);
      _timer = _fireDelay;
    }
  }

  public override void OnCastOff() {
    // remove from inventory and equipment
  }
}