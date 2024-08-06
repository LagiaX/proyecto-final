using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPresentation : MonoBehaviour {
  private Animator animator;

  void Awake() {
    animator = GetComponent<Animator>();  
  }
  void Start() {
    animator.SetTrigger("Activated");
  }
}
