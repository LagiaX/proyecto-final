using UnityEngine;

public class Proyectile : MonoBehaviour, IShootable {
  public int power;
  public float speed;
  public float weaponRange;

  protected bool isFired = false;
  private float totalTravelDistance = 0;

  void Update() {
    if (isFired) _Move();
  }

  private void _Move() {
    totalTravelDistance += speed * Time.deltaTime;
    _CheckLimit();
    transform.position += transform.forward * speed * Time.deltaTime;
  }

  private void _CheckLimit() {
    if (totalTravelDistance >= weaponRange)
      Remove();
  }

  public void OnShoot(float delay) {
    // play shoot animation
    isFired = true;
  }

  public void OnImpact(float speed) {
    print("BANG with " + power + " power and " + speed + " speed");
    // TODO: print decal depending on collided surface
  }

  public void OnCollisionEnter(Collision collision) {
    OnImpact(speed);
    IDamageable target = collision.gameObject.GetComponent<IDamageable>();
    if (target != null) {
      target.OnDamage(power);
    }
    // SFX for impact (?)
    Remove();
  }

  public async void Remove() {
    await GarbageManager.RemoveInTime(gameObject, 1f);
  }
}