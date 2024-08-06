using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour {

  public void OnNewGame() {
    SceneManager.LoadScene("Level_1");
  }

  public void OnLoad() { }

  public void OnOptions() { }

  public void OnExitGame() {
    Application.Quit();
  }
}
