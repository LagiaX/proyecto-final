using UnityEngine;

public class RetryMenu : MonoBehaviour {

  public async void OnRetry() {
    GameManager.resumingSavedGame = false;
    SavefileManager.instance.InitSavefile();
    GameManager.OnGamePause(false);
    await GameManager.ChangeSceneAsync(AppConfig.Level[0]);
    GameManager.LoadPlayerParameters();
  }
}
