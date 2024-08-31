using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager instance;
  public static bool isPaused = false;

  public delegate void PauseGame(bool isPaused);

  void Awake() {
    if (instance == null) {
      instance = this;
      OrganicTarget.Dead += OnNpcDeath;
      PlayerStats.PlayerDead += OnPlayerDead;
      Controls.Pause += OnGamePause;
      PauseMenu.Pause += OnGamePause;
      return;
    }
    Destroy(gameObject);
  }

  public void OnPlayerDead(PlayerStats ps) {
    PlayerSystems.instance.actions.enabled = false;
    PlayerSystems.instance.movement.enabled = false;
    ps.enabled = false;
    Utils.DelayFor(() => SpawnManager.instance.Respawn(), TimeSpan.FromSeconds(2f));
  }

  // TODO: This should be in OrganicTarget, not here
  public async void OnNpcDeath(OrganicTarget ot) {
    await GarbageManager.RemoveInTime(ot.gameObject, 2f);
  }

  public void OnGamePause(bool _isPaused) {
    isPaused = _isPaused;

    if (isPaused) {
      Time.timeScale = 0;
    }
    else {
      Time.timeScale = 1f;
    }
  }

  public static async Task ChangeSceneAsync(string name) {
    AsyncOperation op = SceneManager.LoadSceneAsync(name);
    while (!op.isDone) {
      print(op.progress);
      await Task.Yield();
    }
  }

  public static void ChangeScene(string name) {
    SceneManager.LoadScene(name);
  }

  public static void GoToTitleScreen() {
    ChangeScene(AppConfig.TitleScreen);
  }

  public async static void GoToLevel(int level) {
    await ChangeSceneAsync(AppConfig.Level[level - 1]);
    instance.OnGamePause(false);
  }

  public static void CloseGame() {
    Application.Quit();
  }
}
