using UnityEngine;

public class Coin : Item {
  public delegate void CoinCollect(GameObject g);
  public static event CoinCollect Collected;

  public int value = 1;
  public AudioSource coinSFX;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out OrganicTarget collector)) {
      OnCollect(collector);
    }
  }

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerInventory inventory)) {
      coinSFX.Play();
      inventory.AddCoinValue(value);
      Collected?.Invoke(gameObject);
      Remove();
    }
  }
}
