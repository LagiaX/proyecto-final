using UnityEngine;

public class PlayerRay : MonoBehaviour {

  public static bool isGrounded;

  [Header("Interactions")]
  public float groundDetectionRange;
  public Color colorDetectionRange;
  public bool drawRaycast = false;

  [Header("Slope Metrics")]
  public RaycastHit slopeData;
  public float maxClimbableAngle;
  public float groundCheckDistance;
  public float slopeAngle;

  [Header("Metrics")]
  private float height;
  private float width;
  private float radius;

  private new Collider collider;

  void Start() {
    if (!PlayerSystems.instance.player.TryGetComponent(out collider)) {
      Utils.MissingComponent(typeof(Collider).Name, PlayerSystems.instance.player.name);
    }
    _CalculateMetrics();
  }

  void Update() {
    _DetectGround();
  }

  private void _DetectGround() {
    RaycastHit data; // raycast data is saved here
    if (Physics.SphereCast(transform.position, radius - 0.01f, -transform.up, out data, groundDetectionRange)) {
      // grounded, check slope angle
      if (Physics.Raycast(transform.position, -transform.up, out slopeData, groundCheckDistance)) {
        slopeAngle = Vector3.Angle(transform.up, slopeData.normal);
      }
      isGrounded = slopeAngle < maxClimbableAngle;
      if (drawRaycast) Debug.DrawRay(transform.position, -transform.up * (data.distance + radius), Color.green);
    }
    else {
      // not grounded
      isGrounded = false;
      slopeAngle = 90;
      if (drawRaycast) Debug.DrawRay(transform.position, -transform.up * (groundDetectionRange + radius), colorDetectionRange);
    }
  }

  private void _CalculateMetrics() {
    height = collider.bounds.size.y;
    width = collider.bounds.size.x;
    radius = width / 2;
    groundDetectionRange = height / 2 - radius + 0.015f;
  }
}
