using UnityEngine;

public class TitleScreenMenu : MonoBehaviour {

  private Animator _animator;

  void Awake() {
    _animator = GetComponent<Animator>();
  }

  void Start() {
    GameManager.ShowCursor = true;
    //_animator.Play("Title");
  }

  public async void OnNewGame() {
    GameManager.resumingSavedGame = false;
    SavefileManager.instance.InitSavefile();
    GameManager.OnGamePause(false);
    await GameManager.ChangeSceneAsync(AppConfig.Level[0]);
    GameManager.LoadPlayerParameters();
  }

  public async void OnLoad() {
    if (SavefileManager.instance.LoadGame()) {
      GameManager.resumingSavedGame = true;
      GameManager.OnGamePause(false);
      await GameManager.ChangeSceneAsync(SavefileManager.instance.savefile.systems.scene);
      GameManager.LoadPlayerParameters();
    }
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
