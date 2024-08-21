using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public new Rigidbody rigidbody;

  void Awake() {
    if (!PlayerSystems.instance.player.TryGetComponent(out rigidbody))
      Utils.MissingComponent(typeof(Rigidbody).Name, PlayerSystems.instance.player.name);
  }

  void Start() {

  }

  void Update() {
    
  }
}
