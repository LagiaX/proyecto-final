using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Utils {

  public static StatsPlayer GetPlayerBaseStats() {
    return new StatsPlayer("character", 8, 15);
  }

  public static Health GetPlayerBaseHealth() {
    return new Health(28, 13);
  }

  public static StatsNpc GetNPCBaseStats() {
    return new StatsNpc("npc", 2, 10, 1, 2, 2, 10);
  }

  public static Health GetNPCBaseHealth(EnemyType e) {
    Health health = new Health(1, 0);
    switch (e) {
      case EnemyType.Humanoid:
        health = new Health(5, 0);
        break;
      case EnemyType.Beast:
        health = new Health(4, 0);
        break;
      case EnemyType.Reptile:
        health = new Health(2, 0);
        break;
      case EnemyType.Insect:
        health = new Health(1, 0);
        break;
      case EnemyType.Formless:
        health = new Health(4, 0);
        break;
      case EnemyType.Mechanical:
        health = new Health(7, 0);
        break;
    }
    return health;
  }

  public static void MissingComponent(string componentName, string objectName) {
    Debug.Log($"Missing {componentName} component in {objectName}.");
  }

  public static Transform GetClosestGameObjectFromList(Vector3 position, List<Transform> positionList) {
    Transform t = positionList[0];
    float minDist = Mathf.Infinity;
    foreach (Transform _t in positionList) {
      float distance = Vector3.Distance(position, _t.transform.position);
      if (distance < minDist) {
        t = _t;
        minDist = distance;
      }
    }
    return t;
  }

  public static async void DelayFor(Action act, TimeSpan delay) {
    await Task.Delay(delay);
    act();
  }
}
