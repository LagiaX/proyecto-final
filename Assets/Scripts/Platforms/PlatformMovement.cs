using System;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

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
      if (hasDelay) {
        _timer = delay;
      }
    }
  }

  private void _MoveToNextPoint() {
    transform.position = Vector3.MoveTowards(
      transform.position,
      positions[_currentPosition],
      movementSpeed * Time.deltaTime
    );
  }

  private void _CheckDistance() {
    if (Vector3.Distance(transform.position, positions[_currentPosition]) <= 0.01f) {
      transform.position = positions[_currentPosition];
      isActive = nonStop;
      _currentPosition = (_currentPosition + 1) % positions.Length;
    }
  }

  public void ActivatePlatform(bool nonStop) {
    isActive = true;
    this.nonStop = nonStop;
  }
}
