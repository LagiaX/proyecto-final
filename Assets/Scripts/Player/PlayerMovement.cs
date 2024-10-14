using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public float moveSpeedFinal;
  public Vector3 velocity;
  public Vector3 direction;
  public float facingSpeed;
  public float moveSpeedMod;
  public float jumpDistance;
  public new Rigidbody rigidbody;

  private Transform _target;

  void OnEnable() {
    PlayerActions.ActionLock += _OnTargetLock;
    Controls.Move += _Move;
    Controls.MoveRelease += _MoveRelease;
  }

  void OnDisable() {
    PlayerActions.ActionLock -= _OnTargetLock;
    Controls.Move -= _Move;
    Controls.MoveRelease -= _MoveRelease;
  }

  void Start() {
    if (!PlayerSystems.instance.player.TryGetComponent(out rigidbody))
      Utils.MissingComponent(typeof(Rigidbody).Name, PlayerSystems.instance.player.name);
  }

  void Update() {
    if (_target != null) {
      transform.LookAt(_target.transform);
      PlayerSystems.instance.weaponSlot.transform.forward = _target.transform.position - transform.position;
    }
    _CalculateVelocity();
    rigidbody.velocity = velocity;
  }

  private void _OnTargetLock(Transform target) {
    _target = target;
    moveSpeedMod = target == null ? 1f : 0.75f;
  }

  private void _CalculateMoveDirection(float x, float z) {
    Vector3 cameraForward = new(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
    Vector3 cameraRight = new(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
    direction = (cameraForward * z + cameraRight * x).normalized;
  }

  private void _ChangeFaceDirection() {
    if (_target == null) {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), facingSpeed / 1000);
    }
  }

  public void _CalculateVelocity() {
    float verticalVelocity = velocity.y;
    velocity = direction * moveSpeedFinal;
    velocity = Vector3.ProjectOnPlane(velocity, PlayerSystems.instance.ray.slopeData.normal);
    velocity.y = verticalVelocity;
  }

  private void _Move(float x, float z) {
    _CalculateMoveDirection(x, z);
    _ChangeFaceDirection();
    moveSpeedFinal = PlayerSystems.instance.stats.stats.movementSpeed * moveSpeedMod;
  }

  private void _MoveRelease() {
    moveSpeedFinal = 0;
  }

  public void Jump() {
    if (!PlayerRay.isGrounded) return;
    velocity.y = Mathf.Sqrt(jumpDistance * 2 * -PlayerGravity.gravity);
  }

  public void JumpRelease() {
    if (velocity.y > 0) {
      velocity.y /= 2f;
    }
  }
}
