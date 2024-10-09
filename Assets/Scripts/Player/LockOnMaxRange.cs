using UnityEngine;

public class LockOnMaxRange : MonoBehaviour {
  public PlayerActions player;
  public Collider sphereRange;

  void Awake() {
    if (!TryGetComponent(out sphereRange))
      Utils.MissingComponent(typeof(Collider).Name, name);
  }

  void Start() {
    if (!(player = PlayerSystems.instance.actions))
      Utils.MissingComponent(typeof(PlayerActions).Name, PlayerSystems.instance.name);
    sphereRange.transform.localScale = Vector3.one * player.maxLockOnRange;
  }

  void OnTriggerExit(Collider other) {
    ITargetable t;
    if (other.gameObject.TryGetComponent(out t) && (t as Target).transform == player.target) {
      if (t.GetType() == typeof(EnemyStats)) {
        player.RemoveEnemyOutOfRange((OrganicTarget)t);
        player.ToggleLock();
        return;
      }
      player.RemoveTargetOutOfRange((Target)t);
      player.ToggleLock();
    }
  }
}
