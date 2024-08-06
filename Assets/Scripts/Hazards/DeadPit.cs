using UnityEngine;

public class DeadPit : MonoBehaviour {

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
  }

  private void _DebugMode(bool option) {
    if (gameObject.TryGetComponent(out Renderer r)) {
      r.enabled = true;
    }
  }

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PlayerStats player)) {
      player.OnDamage(player.health.healthCurrent);
    }
  }
}
