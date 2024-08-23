using System.Collections;
using System.Collections.Generic;
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

  void Update() {
    _DetectInputs();
  }

  private void _DetectInputs() {
    float controlHorizontal = Input.GetAxisRaw(AppConfig.ActionMoveHorizontal);
    float controlVertical = Input.GetAxisRaw(AppConfig.ActionMoveVertical);
    if (controlHorizontal != 0 || controlVertical != 0)
      Move?.Invoke(controlHorizontal, controlVertical);
    if (Input.GetKeyDown(AppConfig.keyBindings[AppConfig.ActionJump]))
      Jump?.Invoke();
    if (Input.GetKeyDown(AppConfig.keyBindings[AppConfig.ActionShoot]))
      Shoot?.Invoke();
    if (Input.GetKeyDown(AppConfig.keyBindings[AppConfig.ActionChangeWeapon]))
      ChangeWeapon?.Invoke();
    if (Input.GetKeyDown(AppConfig.keyBindings[AppConfig.ActionLockOn]))
      LockOn?.Invoke();
    // TODO: This should pause the game
    if (Input.GetKeyDown(KeyCode.Escape))
      Application.Quit();
  }
}
