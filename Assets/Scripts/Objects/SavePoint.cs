using UnityEngine;

public class SavePoint : MonoBehaviour {

  public delegate void OnSavePointEnter();
  public static event OnSavePointEnter SavePointEntered;
  public delegate void OnSavePointExit();
  public static event OnSavePointExit SavePointExited;

  public void OnTriggerEnter(Collider other) {
    if (other.gameObject.TryGetComponent(out PlayerStats p)) {
      SavePointEntered?.Invoke();
    }
  }

  public void OnTriggerStay(Collider other) {
    if (other.gameObject.TryGetComponent(out PlayerStats p)) {
      if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.Shoot])) {
        SavefileManager.instance.SaveGame();
      }
    }
  }

  public void OnTriggerExit(Collider other) {
    if (other.gameObject.TryGetComponent(out PlayerStats p)) {
      SavePointExited?.Invoke();
    }
  }
}
