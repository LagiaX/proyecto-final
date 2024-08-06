using UnityEngine;

public class InteractableObject : Object, IInteractable {
  public void OnInteract() {
    print("Interacting with " + gameObject.name);
  }
}
