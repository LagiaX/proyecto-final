using System;
using UnityEngine;

public class Pit : MonoBehaviour {

  public delegate void Fall();
  public static event Fall PlayerFall;
  public delegate void Reposition(Vector3 pos);
  public static event Reposition PlayerReposition;

  public Transform[] repositionPoints;

  private Vector3 _currentPoint;

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
  }

  void Awake() {
    if (repositionPoints.Length == 0)
      Debug.Log("Missing reposition point for pit " + gameObject.name);
  }

  void Start() {
    _currentPoint = repositionPoints[0].position;
  }

  private void _DebugMode(bool option) {
    if (gameObject.TryGetComponent(out Renderer pitRenderer)) {
      pitRenderer.enabled = true;
    }
    for (int i = 0; i < repositionPoints.Length; i++) {
      if (repositionPoints[i].gameObject.TryGetComponent(out Renderer repositionPointRenderer)) {
        repositionPointRenderer.enabled = true;
      }
    }
  }

  private void _RepositionPlayer(Transform player) {
    player.position = _currentPoint;
    PlayerReposition?.Invoke(player.position);
  }

  public void UpdateCheckpoint(Transform t) {
    _currentPoint = t.position;
  }

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PlayerSystems player)) {
      PlayerFall?.Invoke();
      // play SFX for fall
      float sfxDuration = 3f;
      Utils.DelayFor(() => {
        // TODO: Check the case in which the player died by poison mid-fall
        player.stats.OnDamage(AppConfig.PitDamage);
        if (player.stats.health.IsAlive()) {
          _RepositionPlayer(player.transform);
        }
      }, TimeSpan.FromSeconds(sfxDuration));
    }
  }
}
