using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {
  public Transform player;
  public Vector3 offset = new Vector3(0, 4, -4);
  public bool stopFollowing;

  void Awake() {
    OrganicTarget.Alive += _FindPlayer;
  }

  void Start() {
    StartFollowing();
  }

  void LateUpdate() {
    _UpdateCamera();
  }

  private void _FindPlayer(OrganicTarget t) {
    if (t.gameObject.GetComponent<PlayerStats>() != null) {
      player = t.transform;
    }
  }

  private void _UpdateCamera() {
    if (player == null || stopFollowing) return;
    // TODO: Change -2 for value depending on the level height
    //if (player.position.y < -2) return;
    transform.position = player.position + offset;
    transform.LookAt(player);
  }

  public void StartFollowing() { stopFollowing = false; }

  public void StopFollowing() { stopFollowing = true; }
}
