using UnityEngine;

public abstract class Object : MonoBehaviour {
  Material material;

  private void Awake() {
    if (!TryGetComponent(out Renderer renderer)) {
      Utils.MissingComponent(typeof(Renderer).Name, this.GetType().Name);
      return;
    }
    material = renderer.material;
  }
}
