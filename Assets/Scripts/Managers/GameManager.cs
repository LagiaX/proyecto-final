using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public static GameManager instance;
  public static bool isPaused = false;
  public static bool resumingSavedGame = false;

  public delegate void PauseGame(bool isPaused);

  void Awake() {
    if (instance == null) {
      instance = this;
      OrganicTarget.Dead += OnNpcDeath;
      PlayerStats.PlayerDead += OnPlayerDead;
      Controls.Pause += OnGamePause;
      PauseMenu.Pause += OnGamePause;
      return;
    }
    Destroy(gameObject);
  }

  public static void OnPlayerDead(PlayerStats ps) {
    PlayerSystems.instance.actions.enabled = false;
    PlayerSystems.instance.movement.enabled = false;
    ps.enabled = false;
    Utils.DelayFor(() => SpawnManager.instance.Respawn(), TimeSpan.FromSeconds(2f));
  }

  // TODO: This should be in OrganicTarget, not here
  public static async void OnNpcDeath(OrganicTarget ot) {
    await GarbageManager.RemoveInTime(ot.gameObject, 2f);
  }

  public static void OnGamePause(bool _isPaused) {
    isPaused = _isPaused;

    if (isPaused) {
      Time.timeScale = 0;
    }
    else {
      Time.timeScale = 1f;
    }
  }

  public static async Task ChangeSceneAsync(string name) {
    AsyncOperation op = SceneManager.LoadSceneAsync(name);
    while (!op.isDone) {
      print(op.progress);
      await Task.Yield();
    }
  }

  public static void ChangeScene(string name) {
    SceneManager.LoadScene(name);
  }

  public static void GoToTitleScreen() {
    ChangeScene(AppConfig.TitleScreen);
  }

  public static void LoadPlayerParameters() {
    //PlayerSystems player;
    SaveFile sf = SavefileManager.instance.savefile;

    // default values
    Vector3 position = LevelManager.LevelStartingPoint;
    Vector3 rotation = Vector3.zero;
    Health health = Utils.GetPlayerBaseHealth();
    int coins = 0;
    ManaCircuitInventory[] manaCircuits = new ManaCircuitInventory[LevelManager.LevelInfo.totalCircuits];
    for (int i = 0; i < LevelManager.LevelInfo.totalCircuits; i++) {
      manaCircuits[i] = new ManaCircuitInventory(i, false);
    }
    WeaponType[] weapons = new WeaponType[2] { WeaponType.Unarmed, WeaponType.Unarmed };

    // loaded values from savefile, if applicable
    if (resumingSavedGame) {
      position = sf.player.position;
      rotation = sf.player.rotation;
      health = sf.player.health;
      coins = sf.player.inventory.coins;
      for (int i = 0; i < sf.player.inventory.manaCircuits.Length; i++) {
        if (sf.player.inventory.manaCircuits[i].collected) {
          manaCircuits[i] = new ManaCircuitInventory(i, true);
        }
      }
      weapons = sf.player.inventory.weapons;
    }

    Inventory inventory = new Inventory(coins, weapons, manaCircuits);

    if (SpawnManager.instance.SpawnPlayer(position, rotation) != null) {
      PlayerSystems.instance.stats.SetHealth(health);
      PlayerSystems.instance.stats.InitStats();
      PlayerSystems.instance.stats.InitBuffs();
      PlayerSystems.instance.stats.InitStatusAilments();
      PlayerSystems.instance.inventory.SetInventory(inventory);
    }
  }

  public static void CloseGame() {
    Application.Quit();
  }
}
