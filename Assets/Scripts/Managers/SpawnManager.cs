using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
  public static SpawnManager instance;

  public GameObject player; // player prefab
  public List<Transform> checkpoints = new List<Transform>();
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

  void Start() {
    Spawn(0);
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
  private void _MoveToPoint(Transform checkpoint) {
    player.transform.position = checkpoint.position;
  }

  public void Spawn(int id) {
    player = Instantiate(player);
    player.name = "Player";
    _MoveToPoint(Utils.GetClosestGameObjectFromList(player.transform.position, checkpoints));
  }

  public void Respawn() {
    PlayerSystems.instance.stats.enabled = true;
    PlayerSystems.instance.stats.InitStats();
    PlayerSystems.instance.stats.InitStatusAilments();
    PlayerSystems.instance.actions.enabled = true;
    Transform closestCheckpoint = Utils.GetClosestGameObjectFromList(player.transform.position, checkpoints);
    _MoveToPoint(closestCheckpoint);
  }
}
