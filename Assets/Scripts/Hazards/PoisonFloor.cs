using UnityEngine;

public class PoisonFloor : MonoBehaviour {

  public int frameDelay = 10;

  private int _frameCounter = 0;

  public void OnTriggerStay(Collider other) {
    if (_frameCounter == 0) {
      _frameCounter = frameDelay;
      if (other.TryGetComponent(out PlayerSystems player)) {
        player.stats.OnNewAilment(Ailment.Poisoned, 6f);
      }
    }
    else {
      _frameCounter--;
    }
  }
}
