using UnityEngine;

public class Target : MonoBehaviour, ITargetable {

  protected bool _isBeingTargeted = false;

  public void OnFocusEnter(Vector3 position) {
    _isBeingTargeted = true;
    // SHOW interface for targeted object
  }

  public void OnFocusExit() {
    _isBeingTargeted = false;
    // HIDE interface for targeted object
  }
}
