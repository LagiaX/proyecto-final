using UnityEngine;

public class UserInterfaceManager : MonoBehaviour {
  public static UserInterfaceManager instance;

  public Canvas pauseMenu;

  private bool _isShowingPauseMenu;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void Start() {
    Controls.Pause += TogglePauseMenu;
    PauseMenu.Pause += TogglePauseMenu;
    TogglePauseMenu(false);
  }

  public void TogglePauseMenu(bool isPaused) {
    _isShowingPauseMenu = isPaused;
    pauseMenu.gameObject.SetActive(_isShowingPauseMenu);
  }
}
