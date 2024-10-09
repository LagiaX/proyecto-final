using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour {

  public static float gravity;

  [Header("Gravity configuration")]
  public float fallSpeedLimit;

  void Start() {
    gravity = -PlayerSystems.instance.movement.jumpDistance * 5;
  }

  void Update() {
    CalculateGravity();
  }

  private void CalculateGravity() {
    if (PlayerRay.isGrounded && PlayerSystems.instance.movement.velocity.y <= 0) {
      PlayerSystems.instance.movement.velocity.y = 0f;
    }
    else if (PlayerSystems.instance.movement.velocity.y > fallSpeedLimit) {
      PlayerSystems.instance.movement.velocity.y += gravity * Time.deltaTime;
    }
  }
}
