using UnityEngine;

public class Controls : MonoBehaviour {
  public delegate void Movement(float h, float v);
  public delegate void ActionJump();
  public delegate void ActionShoot();
  public delegate void ActionChangeWeapon();
  public delegate void ActionLockTarget();
  public static event Movement Move;
  public static event ActionJump Jump;
  public static event ActionShoot Shoot;
  public static event ActionChangeWeapon ChangeWeapon;
  public static event ActionLockTarget LockTarget;
  public static event GameManager.PauseGame Pause;

  void Update() {
    _DetectInputs();
  }

  private void _DetectInputs() {
    if (!GameManager.isPaused) {
      _GameplayInputs();
    }
    else {
      _MenuInputs();
    }
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Pause?.Invoke(!GameManager.isPaused);
    }
  }

  private void _GameplayInputs() {
    float controlHorizontal = Input.GetAxisRaw("Horizontal");
    float controlVertical = Input.GetAxisRaw("Vertical");
    if (controlHorizontal != 0 || controlVertical != 0)
      Move?.Invoke(controlHorizontal, controlVertical);
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.Jump]))
      Jump?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.Shoot]))
      Shoot?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.ChangeWeapon]))
      ChangeWeapon?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.Control.LockTarget]))
      LockTarget?.Invoke();
  }

  private void _MenuInputs() { }
}
