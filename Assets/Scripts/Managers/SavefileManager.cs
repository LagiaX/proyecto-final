using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavefileManager : MonoBehaviour {
  public static SavefileManager instance;

  public delegate void OnSavefileLoad();
  public static event OnSavefileLoad SavefileLoaded;
  public delegate void NoSavefile();
  public static event NoSavefile SavefileNotFound;

  public SaveFile savefile;
  public bool isDebug = false;

  public readonly string savefileRoute = Directory.GetCurrentDirectory() + "/golden.sav";

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
    PuzzleRoom.Resolved += _AddPuzzleRoom;
    EnemyRoom.Resolved += _AddEnemyRoom;
    ManaCircuit.Collected += _AddManaCircuit;
    PowerUp.Collected += _AddPowerUp;
    ActivateObject.Activated += _AddTrigger;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
    PuzzleRoom.Resolved -= _AddPuzzleRoom;
    EnemyRoom.Resolved -= _AddEnemyRoom;
    ManaCircuit.Collected -= _AddManaCircuit;
    PowerUp.Collected -= _AddPowerUp;
    ActivateObject.Activated -= _AddTrigger;
  }

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void Update() {
    // for testing
    if (isDebug) {
      _Controls();
    }
  }

  private void _DebugMode(bool option) {
    isDebug = option;
  }

  private void _Controls() {
    if (Input.GetKeyDown(KeyCode.C)) {
      SaveGame();
    }
    if (Input.GetKeyDown(KeyCode.V)) {
      LoadGame();
    }
  }

  private void _AddPuzzleRoom(int id) {
    savefile.systems.puzzleRooms[id] = true;
  }

  private void _AddEnemyRoom(int id) {
    savefile.systems.enemyRooms[id] = true;
  }

  private async Task _AddCoinsCollected() {
    for (int i = 0; i < savefile.systems.coins.Length; i++) {
      savefile.systems.coins[i] = CoinManager.instance.coinInfo[i].collected;
      await Task.Yield();
    }
  }

  private void _AddManaCircuit(int id) {
    savefile.systems.circuits[id] = true;
  }

  private void _AddPowerUp(int id) {
    savefile.systems.powerUps[id] = true;
  }

  private void _AddTrigger(int id) {
    savefile.systems.triggers[id] = true;
  }

  public void InitSavefile() {
    // default values
    savefile.systems.puzzleRooms = new bool[LevelManager.LevelInfo.totalPuzzleRooms];
    savefile.systems.enemyRooms = new bool[LevelManager.LevelInfo.totalEnemyRooms];
    savefile.systems.coins = new bool[LevelManager.LevelInfo.totalCoins];
    savefile.systems.circuits = new bool[LevelManager.LevelInfo.totalCircuits];
    savefile.systems.powerUps = new bool[LevelManager.LevelInfo.totalPowerUps];
    savefile.systems.triggers = new bool[LevelManager.LevelInfo.totalTriggers];
  }

  public async void SaveGame() {
    PlayerSystems player = PlayerSystems.instance;
    savefile.systems.scene = SceneManager.GetActiveScene().name;
    savefile.systems.enemiesDefeated = GameManager.enemiesDefeated;
    await _AddCoinsCollected();
    savefile.player.health = player.stats.health;
    savefile.player.inventory = player.inventory.inventory;
    savefile.player.position = player.transform.position;
    savefile.player.rotation = player.transform.eulerAngles;
    File.WriteAllText(savefileRoute, JsonUtility.ToJson(savefile, true));
  }

  public bool LoadGame() {
    if (!File.Exists(savefileRoute)) {
      SavefileNotFound?.Invoke();
      return false;
    }

    savefile = JsonUtility.FromJson<SaveFile>(File.ReadAllText(savefileRoute));
    SavefileLoaded?.Invoke();
    return true;
  }
}
