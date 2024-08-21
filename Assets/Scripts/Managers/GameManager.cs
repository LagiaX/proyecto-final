using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  void Awake() {
    if (instance == null) {
      OrganicTarget.Dead += OnNpcDeath;
      PlayerStats.PlayerDead += OnPlayerDead;
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  public void OnPlayerDead(PlayerStats ps) {
    ps.enabled = false;
    Utils.DelayFor(() => SpawnManager.instance.Respawn(), TimeSpan.FromSeconds(2f));
  }

  public async void OnNpcDeath(OrganicTarget ot) {
    await GarbageManager.RemoveInTime(ot.gameObject, 2f);
  }

  public static async Task ChangeSceneAsync(int id) {
    AsyncOperation op = SceneManager.LoadSceneAsync(id);
    while (!op.isDone) {
      print(op.progress);
      await Task.Yield();
    }
  }

  public static void ChangeScene(int id) {
    SceneManager.LoadScene(id);
  }

  public static void ChangeScene(string name) {
    SceneManager.LoadScene(name);
  }

  public static void Exit() {
    Application.Quit();
  }
}
