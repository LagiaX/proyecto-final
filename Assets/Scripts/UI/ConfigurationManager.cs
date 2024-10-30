using System.IO;
using UnityEngine;
using static AppConfig;

public class ConfigurationManager : MonoBehaviour {

  public static SavedConfig config;

  void Awake() {
    LoadConfiguraton();
  }

  public static void SaveConfiguration() {
    config.soundSettings = AppConfig.SoundSettings;
    config.buttonSettings.jump = (int)KeyBindings[Control.Jump];
    config.buttonSettings.shoot = (int)KeyBindings[Control.Shoot];
    config.buttonSettings.changeWeapon = (int)KeyBindings[Control.ChangeWeapon];
    config.buttonSettings.lockTarget = (int)KeyBindings[Control.LockTarget];
    File.WriteAllText(AppConfig.ConfigRoute, JsonUtility.ToJson(config, true));
  }

  public static void LoadConfiguraton() {
    if (!File.Exists(AppConfig.ConfigRoute)) {
      return;
    }

    config = JsonUtility.FromJson<SavedConfig>(File.ReadAllText(AppConfig.ConfigRoute));
    AppConfig.SoundSettings = config.soundSettings;
    KeyBindings[Control.Jump] = (KeyCode)config.buttonSettings.jump;
    KeyBindings[Control.Shoot] = (KeyCode)config.buttonSettings.shoot;
    KeyBindings[Control.ChangeWeapon] = (KeyCode)config.buttonSettings.changeWeapon;
    KeyBindings[Control.LockTarget] = (KeyCode)config.buttonSettings.lockTarget;
  }
}
