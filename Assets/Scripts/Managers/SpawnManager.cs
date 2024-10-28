using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
  public static SpawnManager instance;

  public delegate void OnSpawn(PlayerStats ps);
  public static event OnSpawn PlayerSpawn;
  public delegate void OnRespawn(PlayerStats ps);
  public static event OnRespawn PlayerRespawn;

  public GameObject player; // player prefab
  public GameObject enemy; // enemy prefab
  public List<Transform> checkpoints = new List<Transform>();
  public int startingPoint = 0;
  public bool isDebug = false;

  void Awake() {
    if (instance == null) {
      instance = this;
      Transform[] children = GetComponentsInChildren<Transform>();
      checkpoints.AddRange(children);
      checkpoints.RemoveAt(0);
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
  }

  void Update() {
    // for testing
    if (isDebug) {
      if (Input.GetKeyDown(KeyCode.R)) {
        Respawn();
      }
    }
  }

  private void _DebugMode(bool option) {
    isDebug = option;
    for (int i = 0; i < checkpoints.Count; i++) {
      if (checkpoints[i].gameObject.TryGetComponent(out Renderer r))
        r.enabled = option;
    }
  }

  public GameObject Spawn(SpawnType type, GameObject g, Vector3 position) {
    GameObject instance = Instantiate(g, position, Quaternion.identity);
    switch (type) {
      case SpawnType.Enemy:
        instance.name = "Enemy";
        break;
      case SpawnType.Item:
        instance.name = "Item";
        break;
      case SpawnType.Object:
        instance.name = "Object";
        break;
    }
    return instance;
  }

  public GameObject SpawnPlayer(Vector3 position, Vector3 rotation) {
    GameObject instance = Instantiate(player, position, Quaternion.identity);
    instance.name = AppConfig.PlayerName;
    instance.transform.eulerAngles = rotation;
    PlayerSpawn?.Invoke(PlayerSystems.instance.stats);
    return instance;
  }

  public void Respawn() {
    PlayerSystems.instance.stats.SetHealth(Utils.GetPlayerBaseHealth());
    PlayerSystems.instance.stats.InitStats();
    PlayerSystems.instance.stats.InitBuffs();
    PlayerSystems.instance.stats.InitStatusAilments();
    PlayerSystems.instance.stats.enabled = true;
    PlayerSystems.instance.actions.enabled = true;
    PlayerSystems.instance.movement.enabled = true;
    Transform closestCheckpoint = Utils.GetClosestGameObjectFromList(player.transform.position, checkpoints);
    PlayerSystems.instance.transform.position = closestCheckpoint.position;
    PlayerRespawn?.Invoke(PlayerSystems.instance.stats);
  }
}
