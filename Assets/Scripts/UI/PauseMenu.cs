using UnityEngine;

public class PauseMenu : MonoBehaviour {
  public static event GameManager.PauseGame Pause;

  public Canvas pauseMenu;
  public Canvas options;

  public void OnResume() {
    Pause?.Invoke(false);
  }

  public void OnOptions() {
    // TODO: Activate options canvas or smt
  }
}
