using UnityEngine;

public class Controls : MonoBehaviour {
  public delegate void Movement(float h, float v);
  public delegate void ActionJump();
  public delegate void ActionShoot();
  public delegate void ActionChangeWeapon();
  public delegate void ActionLockOn();
  public static event Movement Move;
  public static event ActionJump Jump;
  public static event ActionShoot Shoot;
  public static event ActionChangeWeapon ChangeWeapon;
  public static event ActionLockOn LockOn;
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
    float controlHorizontal = Input.GetAxisRaw(AppConfig.ActionMoveHorizontal);
    float controlVertical = Input.GetAxisRaw(AppConfig.ActionMoveVertical);
    if (controlHorizontal != 0 || controlVertical != 0)
      Move?.Invoke(controlHorizontal, controlVertical);
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.ActionJump]))
      Jump?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.ActionShoot]))
      Shoot?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.ActionChangeWeapon]))
      ChangeWeapon?.Invoke();
    if (Input.GetKeyDown(AppConfig.KeyBindings[AppConfig.ActionLockOn]))
      LockOn?.Invoke();
  }

  private void _MenuInputs() { }
}
