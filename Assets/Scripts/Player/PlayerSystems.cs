using UnityEngine;

public class PlayerSystems : MonoBehaviour {
  public static PlayerSystems instance;

  public delegate void IsReady();
  public static event IsReady Ready;

  public PlayerStats stats;
  public PlayerActions actions;
  public PlayerInventory inventory;
  public PlayerRay ray;

  void Awake() {
    if (instance == null) {
      instance = this;
      stats = GetComponent<PlayerStats>();
      actions = GetComponent<PlayerActions>();
      inventory = GetComponent<PlayerInventory>();
      ray = GetComponent<PlayerRay>();
      return;
    }
    Destroy(gameObject);
  }

  void Start() {
    Ready?.Invoke();
    SavefileManager sm = FindAnyObjectByType<SavefileManager>();
    if (sm != null) {
      sm.player = this;
    }
  }
}
