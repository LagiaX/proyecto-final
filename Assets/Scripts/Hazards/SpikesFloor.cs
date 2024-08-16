using UnityEngine;

public class SpikesFloor : MonoBehaviour {

  public float bounceSpeed = 15f;

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
    if (other.gameObject.TryGetComponent(out PlayerSystems player)) {
      if (player.stats.health.IsAlive()) {
        player.stats.OnDamage(AppConfig.spikesDamage);
        player.movement.rigidbody.velocity += Vector3.up * bounceSpeed;
      }
    }
  }
}
