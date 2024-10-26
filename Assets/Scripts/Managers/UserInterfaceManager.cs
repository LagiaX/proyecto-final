using UnityEngine;

public class UserInterfaceManager : MonoBehaviour {
  public static UserInterfaceManager instance;

  public CanvasRenderer pauseMenu;

  private bool _isShowingPauseMenu;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    Controls.Pause += TogglePauseMenu;
    PauseMenu.Pause += TogglePauseMenu;
  }

  void OnDisable() {
    Controls.Pause -= TogglePauseMenu;
    PauseMenu.Pause -= TogglePauseMenu;
  }

  void Start() {
    TogglePauseMenu(false);
  }

  public void TogglePauseMenu(bool isPaused) {
    _isShowingPauseMenu = isPaused;
    pauseMenu.gameObject.SetActive(_isShowingPauseMenu);
  }
}
