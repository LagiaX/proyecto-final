using UnityEngine;

public class SpikesFloor : MonoBehaviour {

  public float bounceSpeed = 12f;

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
  }

  private void _DebugMode(bool option) {
    if (gameObject.TryGetComponent(out Renderer spikesCollisionRenderer)) {
      spikesCollisionRenderer.enabled = true;
    }
  }

  public void OnCollisionEnter(Collision other) {
    if (other.gameObject.TryGetComponent(out PlayerSystems player) && player.stats.health.IsAlive()) {
      if (player.stats.health.healthCurrent - AppConfig.SpikesDamage > 0) {
        player.movement.velocity = Vector3.up * bounceSpeed;
      }
      else {
        player.movement.velocity = Vector3.zero;
      }
      player.stats.OnDamage(AppConfig.SpikesDamage);
    }
  }
}
