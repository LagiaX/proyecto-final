using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

  public delegate void OnActionLock(Transform target);
  public static event OnActionLock ActionLock;

  [Header("Weapon")]
  public Weapon weapon;

  [Header("Locked target")]
  public Transform target;
  public float lockOnRange = 10f;
  public float maxLockOnRange = 15f;
  public List<Transform> enemiesInRange = new List<Transform>();
  public List<Transform> targetsInRange = new List<Transform>();

  void OnEnable() {
    Controls.Jump += Jump;
    Controls.JumpRelease += JumpRelease;
    Controls.Shoot += Shoot;
    Controls.ChangeWeapon += ChangeWeapon;
    Controls.LockTarget += ToggleLock;
    OrganicTarget.Dead += RemoveEnemyOutOfRange;
  }

  void OnDisable() {
    Controls.Jump -= Jump;
    Controls.JumpRelease -= JumpRelease;
    Controls.Shoot -= Shoot;
    Controls.ChangeWeapon -= ChangeWeapon;
    Controls.LockTarget -= ToggleLock;
    OrganicTarget.Dead -= RemoveEnemyOutOfRange;
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
    PlayerSystems.instance.movement.CalculateDirection(x, z);
  }

  public void Jump() {
    PlayerSystems.instance.movement.Jump();
  }

  public void JumpRelease() {
    PlayerSystems.instance.movement.JumpRelease();
  }

  public void Shoot() {
    weapon?.OnAttack();
  }

  public void ChangeWeapon() {
    PlayerSystems.instance.inventory.ChangeWeapon();
  }

  public void ToggleLock() {
    if (target == null) {
      _AssignTarget();
      ActionLock?.Invoke(target);
    }
    else {
      target = null;
      ActionLock?.Invoke(target);
    }
  }

  public void AddEnemyInRange(OrganicTarget ot) {
    enemiesInRange.Add(ot.transform);
  }

  public void RemoveEnemyOutOfRange(OrganicTarget ot) {
    if (ot.transform == target) {
      ToggleLock();
    }
    enemiesInRange.Remove(ot.transform);
  }

  public void AddTargetInRange(Target t) {
    targetsInRange.Add(t.transform);
  }

  public void RemoveTargetOutOfRange(Target t) {
    targetsInRange.Remove(t.transform);
  }
}
