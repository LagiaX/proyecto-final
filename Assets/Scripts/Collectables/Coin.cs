
using UnityEngine;

public class Coin : Item {

  public int value = 1;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out AliveTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(AliveTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory inventory)) {
      inventory.AddCoinValue(value);
      print("Total coins: " + inventory.inventory.coins);
      Destroy(gameObject);
    }
  }
}
