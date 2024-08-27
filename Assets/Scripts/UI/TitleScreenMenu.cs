using UnityEngine;

public class TitleScreenMenu : MonoBehaviour {

  private Animator _animator;

  void Awake() {
    _animator = GetComponent<Animator>();
  }

  void Start() {
    _animator.StartPlayback();
  }

  public void OnNewGame() {
    GameManager.GoToLevel(1);
  }

  public void OnLoad() {
    // TODO: Load savefile, then load corresponding scene
  }

  public void OnTraining() {
    GameManager.ChangeScene(AppConfig.SceneTraining);
  }

  public void OnExitGame() {
    GameManager.CloseGame();
  }

  public void OnTitleSet() {
    _animator.SetBool("isTitleSet", true);
  }
}
