using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {
  public Transform player;
  public Vector3 offset = new Vector3(0, 4, -4);

  void Awake() {
    PlayerStats.Alive += _FindPlayer;
  }

  void LateUpdate() {
    _UpdateCamera();
  }

  private void _FindPlayer() {
    player = FindAnyObjectByType<PlayerActions>()?.transform;
  }

  private void _UpdateCamera() {
    if (player == null) return;
    // TODO: Change -2 for value depending on the level height
    if (player.position.y < -2) return;
    transform.position = player.position + offset;
    transform.LookAt(player);
  }
}
