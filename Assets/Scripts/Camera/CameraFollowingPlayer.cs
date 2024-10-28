using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {
  public static CameraFollowingPlayer instance;

  public Transform player;
  public float speed;
  public Vector3 offset;
  public bool stopFollowing;
  public bool allowCameraCollision;

  void Awake() {
    if (instance == null) {
      instance = this;
      SpawnManager.PlayerSpawn += _FindPlayer;
      SpawnManager.PlayerRespawn += OnPlayerRespawn;
      PlayerStats.PlayerDead += OnPlayerDead;
      Pit.PlayerFall += OnPlayerFall;
      Pit.PlayerReposition += OnPlayerReposition;
      return;
    }
    Destroy(gameObject);
  }

  void Start() {
    transform.position = player.position + offset;
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
    transform.position = Vector3.Lerp(transform.position, player.position + offset, speed * Time.deltaTime);
    transform.forward = Vector3.Slerp(transform.forward, player.transform.position - transform.position, speed * Time.deltaTime);
    if (allowCameraCollision) {
      _CollisionChecker();
    }
  }

  private void _CollisionChecker() {
    RaycastHit data;
    Debug.DrawRay(player.position, transform.position - player.transform.position);
    if (Physics.Raycast(player.position, transform.position - player.transform.position, out data)) {
      transform.position = Vector3.Lerp(transform.position, data.point, speed * Time.deltaTime);
    }
  }

  public void StartFollowing() { stopFollowing = false; }

  public void StopFollowing() { stopFollowing = true; }

  public void OnPlayerRespawn(PlayerStats ps) { StartFollowing(); }

  public void OnPlayerDead(OrganicTarget ot) { StopFollowing(); }

  public void OnPlayerFall() { StopFollowing(); }

  public void OnPlayerReposition(Vector3 pos) { StartFollowing(); }
}
