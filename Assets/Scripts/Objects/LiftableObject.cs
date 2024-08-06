using UnityEngine;

public class LiftableObject : Object, ILiftable {

  public void OnLift() {
    print("Lifting " + gameObject.name + " up.");
  }

  public void OnPutDown() {
    print("Putting " + gameObject.name + " down.");
  }

  public void OnThrow(float speed) {
    print("Throwing " + gameObject.name + " with " + speed + " speed.");
  }
}
