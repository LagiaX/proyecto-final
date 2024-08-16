using UnityEngine;

public class LockOnRange : MonoBehaviour {
  public PlayerActions player;
  public Collider sphereRange;

  void Awake() {
    if (!transform.parent.TryGetComponent(out player))
      Utils.MissingComponent(typeof(PlayerActions).Name, this.GetType().Name);
    if (!TryGetComponent(out sphereRange))
      Utils.MissingComponent(typeof(Collider).Name, this.GetType().Name);
  }

  void Start() {
    sphereRange.transform.localScale = Vector3.one * player.lockOnRange;
  }

  void OnTriggerEnter(Collider other) {
    if (other.gameObject.TryGetComponent(out ITargetable t) && (Target)t != player.target) {
      if (t.GetType() == typeof(EnemyStats)) {
        player.AddEnemyInRange((OrganicTarget)t);
        return;
      }
      player.AddTargetInRange((Target)t);
    }
  }

  void OnTriggerExit(Collider other) {
    ITargetable t;
    if (other.gameObject.TryGetComponent(out t) && (Target)t != player.target) {
      if (t.GetType() == typeof(EnemyStats)) {
        player.RemoveEnemyOutOfRange((OrganicTarget)t);
        return;
      }
      player.RemoveTargetOutOfRange((Target)t);
    }
  }
}
