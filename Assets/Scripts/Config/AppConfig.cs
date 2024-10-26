using System.Collections.Generic;
using UnityEngine;

public class AppConfig : ScriptableObject {
  public static readonly string PlayerName = "Player";

  // SCENES
  public static readonly string TitleScreen = "Title_Screen";
  public static readonly string[] Level = new string[] { "Level_1" };
  public static readonly string SceneTraining = "Testing_Grounds";

  // OPTIONS
  public static readonly float DefaultBGMVol = 0.5f;
  public static readonly float DefaultSFXVol = 0.5f;

  // HUD
  public static readonly int SpaceBetweenHeartIcons = 20; // in pixels

  // CONTROLS
  public enum Control {
    MoveUp,
    MoveRight,
    MoveDown,
    MoveLeft,
    Jump,
    Shoot,
    ChangeWeapon,
    LockTarget
  };

  public static Dictionary<Control, KeyCode> KeyBindings = new() {
    {Control.MoveUp, KeyCode.W },
    {Control.MoveRight, KeyCode.D },
    {Control.MoveDown, KeyCode.S },
    {Control.MoveLeft, KeyCode.A },
    {Control.Jump, KeyCode.Space},
    {Control.Shoot, KeyCode.I},
    {Control.ChangeWeapon, KeyCode.L},
    {Control.LockTarget, KeyCode.O},
  };

  // DAMAGE VALUES
  public static readonly int EnemyContactDamage = 2;
  public static readonly int PoisonDamage = 1;
  public static readonly float PoisonTickRate = 2f;
  public static readonly int BurnDamage = 3;
  public static readonly float BurnTickRate = 1f;
  public static readonly int PitDamage = 3;
  public static readonly int SpikesDamage = 4;
}
