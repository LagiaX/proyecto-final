using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavefileManager : MonoBehaviour {
  public static SavefileManager instance;

  public SaveFile savefile;
  public PlayerSystems player;
  public bool isDebug = false;

  public readonly string savefileRoute = Directory.GetCurrentDirectory() + "/golden.sav";

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
    //PlayerSystems.Ready += _AssignPlayerSystems;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
    //PlayerSystems.Ready -= _AssignPlayerSystems;
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

  // For the future would be cool to have this functionality
  //private void _AssignPlayerSystems() {
  //  player = PlayerSystems.instance;
  //}

  private void _Controls() {
    if (Input.GetKeyDown(KeyCode.C)) {
      SaveGame();
    }
    if (Input.GetKeyDown(KeyCode.V)) {
      LoadGame();
    }
  }

  public void SaveGame() {
    print("Save game");
    savefile.systems.scene = SceneManager.GetActiveScene().buildIndex;
    // TODO: Savepoints
    // savefile.systems.savePoint = 
    savefile.player.position = player.transform.position;
    savefile.player.rotation = player.transform.eulerAngles;
    savefile.player.profile.name = player.name;
    savefile.player.stats = player.stats.stats;
    savefile.player.inventory = player.inventory.inventory;
    File.WriteAllText(savefileRoute, JsonUtility.ToJson(savefile, true));
  }

  public async void LoadGame() {
    print("Load game");

    if (!File.Exists(savefileRoute)) {
      print("No save file D=");
      return;
    }

    savefile = JsonUtility.FromJson<SaveFile>(File.ReadAllText(savefileRoute));
    // Load scene
    await GameManager.ChangeSceneAsync(savefile.systems.scene);
    // Load character
    LoadPlayer();
  }

  public void LoadPlayer() {
    player.stats.stats = savefile.player.stats;
    player.inventory.inventory = savefile.player.inventory;
    player.transform.position = savefile.player.position;
    player.transform.eulerAngles = savefile.player.rotation;
    //player.stats.health.ModifyMissingHealth(0);
  }
}
