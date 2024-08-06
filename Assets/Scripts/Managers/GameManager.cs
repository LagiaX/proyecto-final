using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
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

  public static void Exit() {
    Application.Quit();
  }
}
