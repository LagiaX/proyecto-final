using UnityEngine;

public class RangedWeapon : Weapon {

  public WeaponType type;
  public float bulletDelay;

  [Header("Ammo")]
  public Proyectile proyectilePrefab;
  public Transform shootingPoint;

  protected bool isBoltReady = false;
  protected float _fireDelay;
  protected float _timer = 0;
  protected Proyectile _lastFiredProyectile;

  public void Start() {
    _assignWeaponStats();
  }

  void Update() {
    _CalculateFiringRate();
  }

  private void _assignWeaponStats() {
    switch (type) {
      case WeaponType.Pistol:
        proyectilePrefab.power = PistolStats.power;
        proyectilePrefab.speed = PistolStats.shootingSpeed;
        proyectilePrefab.weaponRange = PistolStats.shootingRange;
        _fireDelay = 1 / PistolStats.fireRate;
        break;
      case WeaponType.Crossbow:
        proyectilePrefab.power = CrossbowStats.power;
        proyectilePrefab.speed = CrossbowStats.shootingSpeed;
        proyectilePrefab.weaponRange = CrossbowStats.shootingRange;
        _fireDelay = 1 / CrossbowStats.fireRate;
        break;
      case WeaponType.Shotgun:
        proyectilePrefab.power = ShotgunStats.power;
        proyectilePrefab.speed = ShotgunStats.shootingSpeed;
        proyectilePrefab.weaponRange = ShotgunStats.shootingRange;
        _fireDelay = 1 / ShotgunStats.fireRate;
        break;
    }
  }

  private void _CalculateFiringRate() {
    if (CanShoot()) {
      if (type == WeaponType.Crossbow && !isBoltReady) {
        _lastFiredProyectile = Instantiate(
            proyectilePrefab,
            shootingPoint.position,
            shootingPoint.rotation,
            shootingPoint
        );
        isBoltReady = true;
      }
      return;
    }
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
      if (type == WeaponType.Pistol || type == WeaponType.Shotgun) {
        _lastFiredProyectile = Instantiate(proyectilePrefab, shootingPoint.position, shootingPoint.rotation);
      }
      _lastFiredProyectile.gameObject.transform.SetParent(null);
      _lastFiredProyectile?.OnShoot(0);
      isBoltReady = false;
      _timer = _fireDelay;
    }
  }

  public override void OnCastOff() {
    // remove from inventory and equipment
  }
}