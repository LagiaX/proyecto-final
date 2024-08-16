using System.Collections.Generic;
using UnityEngine;

public class AppConfig : MonoBehaviour {
  public static Vector3 titleAnimationFrom = new Vector3(0, -350, 0);
  public static Vector3 titleAnimationTo = new Vector3(0, -120, 0);

  public static readonly string SceneTraining = "Testing Grounds";

  public static readonly float defaultBGMVol = 0.5f;
  public static readonly float defaultSFXVol = 0.5f;

  public static string ActionWalkUP = "WalkUp";
  public static string ActionWalkDOWN = "WalkDown";
  public static string ActionWalkLEFT = "WalkLeft";
  public static string ActionWalkRIGHT = "WalkRight";
  public static string ActionMoveHorizontal = "Horizontal";
  public static string ActionMoveVertical = "Vertical";
  public static string ActionJump = "Jump";
  public static string ActionShoot = "Shoot";
  public static string ActionLockOn = "LockOn";

  public static Dictionary<string, KeyCode> keyBindings = new() {
    {ActionJump, KeyCode.Space},
    {ActionShoot, KeyCode.I},
    {ActionLockOn, KeyCode.O},
  };

  public static readonly int poisonDamage = 1;
  public static readonly float poisonTickRate = 2f;
  public static readonly int pitDamage = 3;
  public static readonly int spikesDamage = 4;

}
