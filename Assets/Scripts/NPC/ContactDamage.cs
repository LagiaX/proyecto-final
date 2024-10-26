using UnityEngine;

public class ContactDamage : MonoBehaviour {

  public int damage;
  public float knockbackPower;

  void Awake() {
    damage = AppConfig.EnemyContactDamage;
  }

  public void OnCollisionEnter(Collision collision) {
    PlayerSystems player;
    if (collision.gameObject.TryGetComponent(out player)) {
      player.stats.OnDamage(damage);
      Vector3 knockbackVelocity = (player.transform.position - transform.position) * knockbackPower;
      knockbackVelocity.y /= 4;
      player.movement.Knockback(knockbackVelocity , 0.5f);
    }
  }
}
