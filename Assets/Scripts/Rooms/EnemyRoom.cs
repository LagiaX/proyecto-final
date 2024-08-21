using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour, IActivatable {

  public bool roomClear;
  public List<OrganicTarget> enemies = new List<OrganicTarget>();
  public RemoteDoor door;

  void Awake() {
    if (roomClear) {
      DestroyRoomLogic(3);
    }
    OrganicTarget.Dead += OnEnemyDead;
  }

  void Update() {
    if (roomClear) {
      DestroyRoomLogic(3);
      return;
    }
    CheckRoom();
  }

  public void CheckRoom() {
    if (enemies.Count == 0) {
      roomClear = true;
      door.OnActivate();
    }
  }

  public void OnEnemyDead(OrganicTarget t) {
    enemies.Remove(t);
  }

  public void OnActivate() {
    for (int i = 0; i < enemies.Count; i++) {
      enemies[i].gameObject.SetActive(true);
    }
  }

  public async void DestroyRoomLogic(int delay) {
    OrganicTarget.Dead -= OnEnemyDead;
    await GarbageManager.RemoveInTime(gameObject, delay);
  }
}
