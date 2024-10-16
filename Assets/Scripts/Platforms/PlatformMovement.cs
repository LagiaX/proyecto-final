using UnityEngine;

public class PlatformMovement : MonoBehaviour, IActivatable {

  public Vector3[] positions;
  public float movementSpeed;
  public float rotationSpeed;
  public bool isActive;

  [Header("Automatic Movement")]
  public bool nonStop;
  public bool hasDelay;
  public float delay = 2f;

  private float _timer;
  private int _currentPosition;

  void Update() {
    if (_timer > 0) {
      _timer -= Time.deltaTime;
      return;
    }
    if (isActive) {
      _MoveToNextPoint();
      _CheckDistance();
    }
  }

  private void _MoveToNextPoint() {
    transform.localPosition = Vector3.MoveTowards(
      transform.localPosition,
      positions[_currentPosition],
      movementSpeed * Time.deltaTime
    );
  }

  private void _CheckDistance() {
    if (Vector3.Distance(transform.localPosition, positions[_currentPosition]) <= 0.01f) {
      transform.localPosition = positions[_currentPosition];
      isActive = nonStop;
      _timer = delay;
      _currentPosition = (_currentPosition + 1) % positions.Length;
    }
  }

  public void OnActivate() {
    isActive = true;
  }

  public void OnCollisionEnter(Collision collision) {
    if (collision != null) {
      collision.gameObject.transform.SetParent(transform);
    }
  }

  public void OnCollisionExit(Collision collision) {
    if (collision != null) {
      collision.gameObject.transform.parent = null;
    }
  }
}
