using TMPro;
using UnityEngine;

public class SavePointIndicator : MonoBehaviour {

  public TMP_Text indicator;

  void OnEnable() {
    ButtonConfiguration.ControlChanged += _UpdateIndicator;
    SavePoint.SavePointEntered += _ShowIndicator;
    SavePoint.SavePointExited += _HideIndicator;
  }

  void OnDisable() {
    ButtonConfiguration.ControlChanged -= _UpdateIndicator;
    SavePoint.SavePointEntered -= _ShowIndicator;
    SavePoint.SavePointExited -= _HideIndicator;
  }

  void Start() {
    _UpdateIndicator(AppConfig.Control.Shoot);
  }

  void LateUpdate() {
    Vector3 cameraPosition = Camera.main.transform.position;
    transform.LookAt(cameraPosition, Vector3.up);
    transform.forward = -transform.forward;
  }

  private void _UpdateIndicator(AppConfig.Control c) {
    if (c == AppConfig.Control.Shoot) {
      indicator.text = AppConfig.KeyBindings[AppConfig.Control.Shoot] + " - Save game";
    }
  }

  private void _ShowIndicator() {
    indicator.gameObject.SetActive(true);
  }

  private void _HideIndicator() {
    indicator.gameObject.SetActive(false);
  }
}
