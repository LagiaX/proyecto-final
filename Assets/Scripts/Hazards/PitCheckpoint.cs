using UnityEngine;

public class PitCheckpoint : MonoBehaviour {

  public Pit pit;
  public Transform checkpoint;

  public void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out PlayerSystems player)) {
      pit.UpdateCheckpoint(checkpoint);
    }
  }
}
