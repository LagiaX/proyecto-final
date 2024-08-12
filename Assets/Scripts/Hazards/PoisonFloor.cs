using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFloor : MonoBehaviour {

  public void OnCollisionEnter(Collision other) {
    if (other.gameObject.TryGetComponent(out PlayerSystems player)) {
      player.stats.OnNewAilment(Ailment.Poisoned, Mathf.Infinity);
    }
  }

  public void OnCollisionExit(Collision other) {
    if (other.gameObject.TryGetComponent(out PlayerSystems player)) {
      player.stats.OnNewAilment(Ailment.Poisoned, 3f);
    }
  }
}
