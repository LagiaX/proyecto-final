using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  void Awake() {
    if (instance == null) {
      OrganicTarget.Dead += DeadHandler;
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  public async void DeadHandler(OrganicTarget t) {
    t.enabled = false;
    if (t.TryGetComponent(out PlayerSystems player)) {
      Utils.DelayFor(() => SpawnManager.instance.Respawn(), TimeSpan.FromSeconds(2f));
    }
    else if (t.TryGetComponent(out EnemyStats stats)) {
      await GarbageManager.RemoveInTime(t.gameObject, 2f);
    }
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
