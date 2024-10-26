using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
  public static PowerupManager instance;

  public GameObject healthPrefab;
  public GameObject speedPrefab;
  public GameObject pistolPrefab;
  public GameObject crossbowPrefab;
  public PowerupInfo[] powerupInfo;
  public List<GameObject> powerups;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    PowerUp.Collected += _MarkAsCollected;
  }

  void OnDisable() {
    PowerUp.Collected -= _MarkAsCollected;
  }

  private void _MarkAsCollected(int id) {
    powerupInfo[id].collected = true;
  }

  public void GeneratePowerup(Powerup type, int index) {
    GameObject prefab = new GameObject();
    switch (type) {
      case Powerup.Health:
        prefab = healthPrefab;
        break;
      case Powerup.Speed:
        prefab = speedPrefab;
        break;
      case Powerup.Pistol:
        prefab = pistolPrefab;
        break;
      case Powerup.Crossbow:
        prefab = crossbowPrefab;
        break;
    }
    GameObject powerup = Instantiate(prefab, transform);
    powerups.Add(powerup);
    PowerUp p;
    if (powerup.TryGetComponent(out p)) {
      p.id = index;
    }
    powerup.name = powerupInfo[index].name;
    powerup.transform.localPosition = powerupInfo[index].position;
  }
}
