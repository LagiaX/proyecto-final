using System.Collections.Generic;
using UnityEngine;

public class AppConfig : MonoBehaviour {
  public static Vector3 titleAnimationFrom = new Vector3(0, -350, 0);
  public static Vector3 titleAnimationTo = new Vector3(0, -120, 0);

  public static readonly string SceneTraining = "Testing Grounds";

  public static readonly float defaultBGMVol = 0.5f;
  public static readonly float defaultSFXVol = 0.5f;

  public static readonly int spaceBetweenHeartIcons = 20; // in pixels

  public static readonly string playerName = "Player";

  public static readonly string ActionWalkUP = "WalkUp";
  public static readonly string ActionWalkDOWN = "WalkDown";
  public static readonly string ActionWalkLEFT = "WalkLeft";
  public static readonly string ActionWalkRIGHT = "WalkRight";
  public static readonly string ActionMoveHorizontal = "Horizontal";
  public static readonly string ActionMoveVertical = "Vertical";
  public static readonly string ActionJump = "Jump";
  public static readonly string ActionShoot = "Shoot";
  public static readonly string ActionChangeWeapon = "ChangeWeapon";
  public static readonly string ActionLockOn = "LockOn";

  public static Dictionary<string, KeyCode> keyBindings = new() {
    {ActionJump, KeyCode.Space},
    {ActionShoot, KeyCode.I},
    {ActionLockOn, KeyCode.O},
    {ActionChangeWeapon, KeyCode.L},
  };

  public static readonly int poisonDamage = 1;
  public static readonly float poisonTickRate = 2f;
  public static readonly int pitDamage = 3;
  public static readonly int spikesDamage = 4;

}
