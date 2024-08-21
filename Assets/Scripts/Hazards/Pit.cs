using System;
using UnityEngine;

public class Pit : MonoBehaviour {

  public delegate void Fall();
  public static event Fall PlayerFall;
  public delegate void Reposition(Vector3 pos);
  public static event Reposition PlayerReposition;

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
    PlayerReposition?.Invoke(player.position);
  }

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PlayerSystems player)) {
      PlayerFall?.Invoke();
      // play sound FX for fall
      float sfxDuration = 3f;
      Utils.DelayFor(() => {
        // TODO: Check the case in which the player died by poison mid-fall
        player.stats.OnDamage(AppConfig.pitDamage);
        if (player.stats.health.IsAlive()) {
          _RepositionPlayer(player.transform);
        }
      }, TimeSpan.FromSeconds(sfxDuration));
    }
  }
}
