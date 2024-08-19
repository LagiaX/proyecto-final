using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {
  public static CameraFollowingPlayer instance;

  public Transform player;
  public Vector3 offset = new Vector3(0, 4, -4);
  public bool stopFollowing;

  void Awake() {
    if (instance == null) {
      instance = this;
      SpawnManager.PlayerSpawn += _FindPlayer;
      SpawnManager.PlayerRespawn += OnPlayerRespawn;
      PlayerStats.PlayerDead += OnPlayerDead;
      return;
    }
    Destroy(gameObject);
  }

  void LateUpdate() {
    _UpdateCamera();
  }

  private void _FindPlayer(PlayerStats ps) {
    if (ps != null) {
      player = ps.transform;
      StartFollowing();
      return;
    }
    Debug.Log("Missing player position reference");
  }

  private void _UpdateCamera() {
    if (player == null || stopFollowing) return;
    transform.position = player.position + offset;
    transform.LookAt(player);
  }

  public void StartFollowing() { stopFollowing = false; }

  public void StopFollowing() { stopFollowing = true; }

  public void OnPlayerRespawn(PlayerStats ps) {
    StartFollowing();
  }

  public void OnPlayerDead(OrganicTarget ot) {
    StopFollowing();
  }
}
