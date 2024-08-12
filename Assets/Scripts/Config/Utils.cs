using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Utils {

  public static CharacterStats GetCharacterBaseStats() {
    return new CharacterStats("character", 8, 1f, 15);
  }

  public static Health GetCharacterBaseHealth() {
    return new Health(5, 0);
  }

  public static NonPlayableCharacterStats GetNonPlayableCharacterBaseStats() {
    return new NonPlayableCharacterStats("npc", 2, 10, 1, 2, 2, 10);
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
