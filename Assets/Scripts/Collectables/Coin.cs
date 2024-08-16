
using UnityEngine;

public class Coin : Item {

  public int value = 1;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out OrganicTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory inventory)) {
      inventory.AddCoinValue(value);
      Remove();
    }
  }
}
