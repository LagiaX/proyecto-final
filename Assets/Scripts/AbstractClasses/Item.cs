using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable {
  public abstract void OnCollect(OrganicTarget collector);

  public async void Remove() {
    await GarbageManager.RemoveInTime(gameObject, 2f);
  }
}
