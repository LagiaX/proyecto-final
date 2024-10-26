using UnityEngine;

public class PowerUp : Item {
  public delegate void PowerupCollected(int id);
  public static event PowerupCollected Collected;

  [Header("Properties")]
  public int id;
  public bool destroyOnCollect;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out OrganicTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(OrganicTarget collector) {
    if (destroyOnCollect) {
      Collected?.Invoke(id);
      Remove();
    }
  }
}
