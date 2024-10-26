using UnityEngine;

public class PauseMenu : MonoBehaviour {
  public static event GameManager.PauseGame Pause;

  public void OnResume() {
    Pause?.Invoke(false);
  }
}
