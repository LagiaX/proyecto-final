using UnityEngine;

public class DetectionRange : MonoBehaviour {
  public delegate void OnDetect(Transform t);
  public event OnDetect Detected;
  public delegate void OnTargetLose(Transform t);
  public event OnTargetLose TargetLost;

  public void OnTriggerEnter(Collider other) {
    PlayerStats playerStats;
    if (other.TryGetComponent(out playerStats)) {
      Detected?.Invoke(playerStats.transform);
    }
  }

  public void OnTriggerExit(Collider other) {
    PlayerStats playerStats;
    if (other.TryGetComponent(out playerStats)) {
      TargetLost?.Invoke(playerStats.transform);
    }
  }
}
