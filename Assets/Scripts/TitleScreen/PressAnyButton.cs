using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButton : MonoBehaviour {

  public CanvasRenderer menuNewGame;
  public CanvasRenderer menuLoad;
  public CanvasRenderer menuOptions;
  public CanvasRenderer menuExitGame;

  public void OnPressAnyButton() {
    menuNewGame.gameObject.SetActive(true);
    menuLoad.gameObject.SetActive(true);
    menuOptions.gameObject.SetActive(true);
    menuExitGame.gameObject.SetActive(true);
    Destroy(this.gameObject);
  }
}
