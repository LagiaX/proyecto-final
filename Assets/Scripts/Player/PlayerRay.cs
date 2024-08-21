using UnityEngine;

public class PlayerRay : MonoBehaviour {

  [Header("Interactions")]
  public static bool isGrounded;
  public float groundDetectionRange;
  public Color colorDetectionRange;
  public bool drawRaycast = false;

  [Header("Metrics")]
  private float height;
  private float width;
  private float radius;

  private new Collider collider;

  private void Awake() {
    if (!PlayerSystems.instance.player.TryGetComponent(out collider)) {
      Utils.MissingComponent(typeof(Collider).Name, PlayerSystems.instance.player.name);
    }
  }

  void Start() {
    CalculateMetrics();
  }

  void Update() {
    DetectGround();
  }

  private void DetectGround() {
    RaycastHit data; // raycast data is saved here
    if (Physics.SphereCast(transform.position, radius, -transform.up, out data, groundDetectionRange)) {
      if (drawRaycast) Debug.DrawRay(transform.position, -transform.up * (data.distance + radius), Color.green);
      // grounded
      isGrounded = true;
    }
    else {
      if (drawRaycast) Debug.DrawRay(transform.position, -transform.up * (groundDetectionRange + radius), colorDetectionRange);
      isGrounded = false;
    }
  }

  private void CalculateMetrics() {
    height = collider.bounds.size.y;
    width = collider.bounds.size.x;
    radius = width / 2;
    groundDetectionRange = height / 2 - radius + 0.001f;
  }
}
