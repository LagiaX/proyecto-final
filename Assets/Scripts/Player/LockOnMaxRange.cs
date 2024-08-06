using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnMaxRange : MonoBehaviour {
  public PlayerActions player;
  public Collider sphereRange;

  void Awake() {
    if (!transform.parent.TryGetComponent(out player))
      Utils.MissingComponent(typeof(PlayerActions).Name, this.GetType().Name);
    if (!TryGetComponent(out sphereRange))
      Utils.MissingComponent(typeof(Collider).Name, this.GetType().Name);
  }

  void Start() {
    sphereRange.transform.localScale = Vector3.one * player.maxLockOnRange;
  }

  void OnTriggerExit(Collider other) {
    ITargetable t;
    if (other.gameObject.TryGetComponent(out t) && (t as Target).transform == player.target) {
      print("OUT OF MAX RANGED LOCKING ON");
      if (t.GetType() == typeof(EnemyStats)) {
        player.RemoveEnemyOutOfRange((AliveTarget)t);
        player.ToggleLockOn();
        return;
      }
      player.RemoveTargetOutOfRange((Target)t);
      player.ToggleLockOn();
    }
  }
}
