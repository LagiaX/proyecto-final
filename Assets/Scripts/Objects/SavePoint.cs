using UnityEngine;

public class SavePoint : MonoBehaviour {

  public delegate void OnSavePointEnter();
  public static event OnSavePointEnter SavePointEntered;
  public delegate void OnSavePointExit();
  public static event OnSavePointExit SavePointExited;

  public AudioSource gameSavedSFX;

  private bool _isPlayerClose;

  void Update() {
    if (_isPlayerClose && Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.Shoot])) {
      SavefileManager.instance.SaveGame();
      gameSavedSFX.Play();
    }
  }

  public void OnTriggerEnter(Collider other) {
    if (other.gameObject.TryGetComponent(out PlayerStats p)) {
      SavePointEntered?.Invoke();
      _isPlayerClose = true;
    }
  }

  public void OnTriggerExit(Collider other) {
    if (other.gameObject.TryGetComponent(out PlayerStats p)) {
      SavePointExited?.Invoke();
      _isPlayerClose = false;
    }
  }
}
