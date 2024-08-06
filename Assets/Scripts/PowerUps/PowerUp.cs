using UnityEngine;

public class PowerUp : Item {
  [Header("Properties")]
  public bool destroyOnCollect;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out AliveTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(AliveTarget collector) {
    if (destroyOnCollect)
      Destroy(gameObject);
  }
}
