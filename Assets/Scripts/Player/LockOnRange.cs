using UnityEngine;

public class LockOnRange : MonoBehaviour {
  public PlayerActions player;
  public Collider sphereRange;

  void Awake() {
    if (!(player = PlayerSystems.instance.actions))
      Utils.MissingComponent(typeof(PlayerActions).Name, PlayerSystems.instance.name);
    if (!TryGetComponent(out sphereRange))
      Utils.MissingComponent(typeof(Collider).Name, name);
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
