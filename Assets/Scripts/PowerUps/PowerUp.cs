using UnityEngine;

public class PowerUp : Item {
  [Header("Properties")]
  public bool destroyOnCollect;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out OrganicTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(OrganicTarget collector) {
    if (destroyOnCollect)
      Remove();
  }
}
