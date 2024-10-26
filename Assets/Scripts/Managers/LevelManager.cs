using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour {

  public static LevelManager instance;
  public static readonly Vector3 LevelStartingPoint = new Vector3(-3, 9, -30);
  public static readonly LevelInfo LevelInfo = new LevelInfo(0, 2, 1, 85, 3, 5, 1);

  public PuzzleRoom[] puzzleRooms;
  public EnemyRoom[] enemyRooms;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  async void Start() {
    if (GameManager.resumingSavedGame) {
      for (int i = 0; i < puzzleRooms.Length; i++) {
        puzzleRooms[i].OnPuzzleSolved(SavefileManager.instance.savefile.systems.puzzleRooms[i]);
      }
      for (int i = 0; i < enemyRooms.Length; i++) {
        enemyRooms[i].SetRoomClear(SavefileManager.instance.savefile.systems.enemyRooms[i]);
      }
    }
    await InitializeCoins();
    await InitializeCircuits();
    await InitializePowerups();
  }

  public async Task InitializeCoins() {
    CoinManager.instance.coinInfo = new CoinInfo[LevelInfo.totalCoins];
    for (int i = 0; i < LevelInfo.totalCoins; i++) {
      CoinManager.instance.InitCoin(i);
      if (GameManager.resumingSavedGame) {
        CoinManager.instance.coinInfo[i].collected = SavefileManager.instance.savefile.systems.coins[i];
      }
      if (!CoinManager.instance.coinInfo[i].collected) {
        CoinManager.instance.GenerateCoin(i);
      }
      await Task.Yield();
    }
  }

  public async Task InitializeCircuits() {
    ManaCircuitManager.instance.circuitInfo = new CoinInfo[LevelInfo.totalCircuits];
    for (int i = 0; i < LevelInfo.totalCircuits; i++) {
      ManaCircuitManager.instance.InitCircuit(i);
      if (GameManager.resumingSavedGame) {
        ManaCircuitManager.instance.circuitInfo[i].collected = SavefileManager.instance.savefile.systems.circuits[i];
      }
      if (!ManaCircuitManager.instance.circuitInfo[i].collected) {
        ManaCircuitManager.instance.GenerateCircuit(i);
      }
      await Task.Yield();
    }
  }

  public async Task InitializePowerups() {
    for (int i = 0; i < LevelInfo.totalPowerUps; i++) {
      if (GameManager.resumingSavedGame) {
        PowerupManager.instance.powerupInfo[i].collected = SavefileManager.instance.savefile.systems.powerUps[i];
      }
      if (!PowerupManager.instance.powerupInfo[i].collected) {
        PowerupManager.instance.GeneratePowerup(PowerupManager.instance.powerupInfo[i].type, i);
      }
      await Task.Yield();
    }
  }
}
