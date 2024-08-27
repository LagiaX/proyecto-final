using System.Collections.Generic;
using UnityEngine;

public class AppConfig : MonoBehaviour {
  // SCENES
  public static readonly string TitleScreen = "Title_Screen";
  public static readonly string[] Level = new string[]{ "Level_1" , "Level_2" };
  public static readonly string SceneTraining = "Testing_Grounds";

  // OPTIONS
  public static readonly float DefaultBGMVol = 0.5f;
  public static readonly float DefaultSFXVol = 0.5f;

  // HUD
  public static readonly int SpaceBetweenHeartIcons = 20; // in pixels

  // PLAYER ACTIONS
  public static readonly string PlayerName = "Player";

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

  public static Dictionary<string, KeyCode> KeyBindings = new() {
    {ActionJump, KeyCode.Space},
    {ActionShoot, KeyCode.I},
    {ActionLockOn, KeyCode.O},
    {ActionChangeWeapon, KeyCode.L},
  };

  // DAMAGE VALUES
  public static readonly int PoisonDamage = 1;
  public static readonly float PoisonTickRate = 2f;
  public static readonly int PitDamage = 3;
  public static readonly int SpikesDamage = 4;

}
