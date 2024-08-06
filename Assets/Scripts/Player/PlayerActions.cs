using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

  [Header("Movement")]
  public Vector3 direction;
  public float facingSpeed;
  public float moveSpeedMod;
  public float jumpDistance;

  [Header("Weapon")]
  public GameObject weaponSlot;
  public Weapon weaponPrefab;

  [Header("Locked target")]
  public Transform target;
  public float lockOnRange = 10f;
  public float maxLockOnRange = 15f;
  public List<Transform> enemiesInRange = new List<Transform>();
  public List<Transform> targetsInRange = new List<Transform>();

  private PlayerStats _playerStats;
  private PlayerInventory _playerInventory;
  private Rigidbody _rigidbody;
  private float _moveSpeedFinal;

  void OnEnable() {
    Controls.Move += Move;
    Controls.Jump += Jump;
    Controls.Shoot += Shoot;
    Controls.LockOn += ToggleLockOn;
  }

  void OnDisable() {
    Controls.Move -= Move;
    Controls.Jump -= Jump;
    Controls.Shoot -= Shoot;
    Controls.LockOn -= ToggleLockOn;
  }

  void Awake() {
    if (!TryGetComponent(out _rigidbody)) {
      Utils.MissingComponent(typeof(Rigidbody).Name, this.GetType().Name);
    }
    if (!TryGetComponent(out _playerStats)) {
      Utils.MissingComponent(typeof(PlayerStats).Name, this.GetType().Name);
    }
    if (!TryGetComponent(out _playerInventory)) {
      Utils.MissingComponent(typeof(PlayerInventory).Name, this.GetType().Name);
    }
    if (weaponSlot.transform.childCount > 0) {
      for (int i = 0; i < weaponSlot.transform.childCount; i++) {
        GameObject g = weaponSlot.transform.GetChild(i).gameObject;
        if (g.activeInHierarchy) {
          weaponPrefab = g.GetComponent<RangedWeapon>();
        }
      }
    }
    // TODO: proper platformer jump and remove this
    Physics.gravity = new Vector3(0, -30, 0);
  }

  void Start() {
    
  }

  private void _CalculateMovementSpeed() {
    _moveSpeedFinal = _playerStats.stats.movementSpeed * moveSpeedMod;
  }

  private void _CalculateMoveDirection(float x, float z) {
    Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
    Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
    direction = (cameraForward * z + cameraRight * x).normalized;
  }

  private void _ChangeFaceDirection() {
    if (target != null) {
      transform.LookAt(target.transform);
      return;
    }
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), facingSpeed / 1000);
  }

  private void _AssignTarget() {
    if (enemiesInRange.Count > 0) {
      target = Utils.GetClosestGameObjectFromList(transform.position, enemiesInRange);
    }
    else if (targetsInRange.Count > 0) {
      target = Utils.GetClosestGameObjectFromList(transform.position, targetsInRange);
    }
  }

  public void Move(float x, float z) {
    if (this == null) return;
    _CalculateMoveDirection(x, z);
    _ChangeFaceDirection();
    _CalculateMovementSpeed();
    transform.position += direction * _moveSpeedFinal * Time.deltaTime;
    // This solution has problems with gravity and climbing supposedly unclimbable slopes
    //_rb.velocity = direction * _moveSpeedFinal;
  }

  public void Jump() {
    if (!PlayerRay.isGrounded) return;
    _rigidbody.velocity += Vector3.up * Mathf.Sqrt(jumpDistance * 2 * -Physics.gravity.y);
  }

  public void Shoot() {
    // TODO: This should be in the inventory. Have a pointer to the inventory and access it when needed.
    weaponPrefab?.OnAttack();
  }

  public void ToggleLockOn() {
    if (target == null) {
      moveSpeedMod = 0.75f;
      _AssignTarget();
      _ChangeFaceDirection();
    }
    else {
      moveSpeedMod = 1f;
      target = null;
    }
  }

  public void AddEnemyInRange(AliveTarget at) {
    enemiesInRange.Add(at.transform);
    AliveTarget.Dead += RemoveEnemyOutOfRange;
  }

  public void RemoveEnemyOutOfRange(AliveTarget at) {
    enemiesInRange.Remove(at.transform);
  }

  public void AddTargetInRange(Target t) {
    targetsInRange.Add(t.transform);
  }

  public void RemoveTargetOutOfRange(Target t) {
    targetsInRange.Remove(t.transform);
  }
}