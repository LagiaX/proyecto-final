using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class Pit : MonoBehaviour {

  public Transform repositionPoint;

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
  }

  void Awake() {
    if (repositionPoint == null)
      Debug.Log("Missing reposition point for pit " + gameObject.name);
  }

  private void _DebugMode(bool option) {
    if (gameObject.TryGetComponent(out Renderer pitRenderer)) {
      pitRenderer.enabled = true;
    }
    if (repositionPoint.gameObject.TryGetComponent(out Renderer repositionPointRenderer)) {
      repositionPointRenderer.enabled = true;
    }
  }

  private void _RepositionPlayer(Transform player) {
    player.position = repositionPoint.position;
  }

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PlayerSystems player)) {
      // play sound FX for fall
      float sfxDuration = 3f;
      Utils.DelayFor(() => {
        player.stats.OnDamage(3);
        if (player.stats.health.IsAlive()) {
          _RepositionPlayer(player.transform);
        }
      }, TimeSpan.FromSeconds(sfxDuration));
    }
  }
}
