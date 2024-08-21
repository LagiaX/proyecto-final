using UnityEngine;

public class PlayerSystems : MonoBehaviour {
  public static PlayerSystems instance;

  public delegate void IsReady();
  public static event IsReady Ready;

  public GameObject player;
  public GameObject weaponSlot;

  [Header("Systems")]
  public PlayerStats stats;
  public PlayerActions actions;
  public PlayerMovement movement;
  public PlayerInventory inventory;
  public PlayerRay ray;

  void Awake() {
    if (instance == null) {
      instance = this;
      if (player == null) {
        player = transform.Find("PlayerModel").gameObject;
      }
      if (weaponSlot == null) {
        weaponSlot = transform.Find("WeaponSlot").gameObject;
      }
      stats = GetComponent<PlayerStats>();
      actions = GetComponent<PlayerActions>();
      movement = GetComponent<PlayerMovement>();
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
