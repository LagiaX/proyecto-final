using UnityEngine;

public class TitleScreenMenu : MonoBehaviour {

  public void OnNewGame() {
    GameManager.ChangeScene(1);
  }

  public void OnLoad() { }

  public void OnTraining() {
    GameManager.ChangeScene(AppConfig.SceneTraining);
  }

  public void OnOptions() { }

  public void OnExitGame() {
    GameManager.Exit();
  }
}
