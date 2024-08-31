using UnityEngine;
using static AppConfig;

public class Controls : MonoBehaviour {
  public delegate void OnMove(float h, float v);
  public static event OnMove Move;
  public delegate void OnMoveRelease();
  public static event OnMoveRelease MoveRelease;
  public delegate void OnJump();
  public static event OnJump Jump;
  public delegate void OnJumpRelease();
  public static event OnJumpRelease JumpRelease;
  public delegate void OnShoot();
  public static event OnShoot Shoot;
  public delegate void OnChangeWeapon();
  public static event OnChangeWeapon ChangeWeapon;
  public delegate void OnLockTarget();
  public static event OnLockTarget LockTarget;

  public static event GameManager.PauseGame Pause;

  private Vector3 _lastDirection;

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
    if (controlHorizontal != 0 || controlVertical != 0) {
      _lastDirection = controlHorizontal * Vector3.right + controlVertical * Vector3.forward;
      Move?.Invoke(controlHorizontal, controlVertical);
    }
    else if (_lastDirection != Vector3.zero) {
      _lastDirection = Vector3.zero;
      MoveRelease?.Invoke();
    }

    if (Input.GetKeyDown(KeyBindings[Control.Jump]))
      Jump?.Invoke();
    if (Input.GetKeyUp(KeyBindings[Control.Jump]))
      JumpRelease?.Invoke();
    if (Input.GetKeyDown(KeyBindings[Control.Shoot]))
      Shoot?.Invoke();
    if (Input.GetKeyDown(KeyBindings[Control.ChangeWeapon]))
      ChangeWeapon?.Invoke();
    if (Input.GetKeyDown(KeyBindings[Control.LockTarget]))
      LockTarget?.Invoke();
  }

  private void _MenuInputs() { }
}
