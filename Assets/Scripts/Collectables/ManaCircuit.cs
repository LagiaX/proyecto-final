using UnityEngine;

public class ManaCircuit : Item {
  public delegate void CircuitCollect(int circuitNumber);
  public static event CircuitCollect Collected;

  public int id;
  public int value = 30;
  public bool collected;

  private void Awake() {
    if (collected) {
      Destroy(gameObject);
    }
  }

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out OrganicTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(OrganicTarget collector) {
    // TODO: Collectables shouldn't access the player's inventory, they should just notify as Collected
    // the inventory should listen to collectable events and update its values accordingly
    if (collector.TryGetComponent(out PlayerInventory inventory)) {
      collected = true;
      inventory.AddCoinValue(value);
      inventory.AddManaCircuit(new int[] { id });
      Collected?.Invoke(id);
      Remove();
    }
  }
}
