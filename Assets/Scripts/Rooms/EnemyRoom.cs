using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyRoom : MonoBehaviour, IActivatable {
  public delegate void RoomResolved(int id);
  public static event RoomResolved Resolved;

  public int id;
  public List<OrganicTarget> enemies = new List<OrganicTarget>();
  public RemoteDoor[] doors;

  void OnEnable() {
    OrganicTarget.Dead += OnEnemyDead;
  }

  void OnDisable() {
    OrganicTarget.Dead -= OnEnemyDead;
  }

  public void SetRoomClear(bool isRoomClear) {
    if (isRoomClear) {
      int totalEnemies = enemies.Count;
      for (int i = 0; i < totalEnemies; i++) {
        enemies.ElementAt(0).OnDeath();
      }
    }
  }

  public void CheckRoom() {
    if (enemies.Count == 0) {
      ActivateDoors();
      Resolved?.Invoke(id);
      DestroyRoomLogic(3);
    }
  }

  public void ActivateDoors() {
    for (int i = 0; i < doors.Length; i++) {
      doors[i].OnActivate();
    }
  }

  public void OnEnemyDead(OrganicTarget t) {
    enemies.Remove(t);
    CheckRoom();
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
