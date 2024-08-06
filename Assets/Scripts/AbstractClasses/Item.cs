using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable {
  public abstract void OnCollect(AliveTarget collector);
}
