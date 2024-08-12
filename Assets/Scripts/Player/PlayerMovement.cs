using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public new Rigidbody rigidbody;

  void Awake() {
    if (!TryGetComponent(out rigidbody))
      Utils.MissingComponent(typeof(Rigidbody).Name, this.GetType().Name);
  }

  void Start() {

  }

  void Update() {
    
  }
}
