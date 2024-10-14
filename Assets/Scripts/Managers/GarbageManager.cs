using System.Threading.Tasks;
using UnityEngine;

public class GarbageManager : MonoBehaviour {
  public static GarbageManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  public static async Task RemoveInTime(GameObject g, float time) {
    _Clean(g);
    await Task.Delay((int)(time * 1000));
    _Remove(g);
  }

  private static void _Clean(GameObject g) {
    Collider collider = g.GetComponent<Collider>();
    if (collider) {
      collider.enabled = false;
    }
    Renderer renderer = g.GetComponent<Renderer>();
    if (renderer) {
      renderer.enabled = false;
    }
    Light light = g.GetComponent<Light>();
    if (light) {
      light.enabled = false;
    }

    for (int i = 0; i < g.transform.childCount; i++) {
      _Clean(g.transform.GetChild(i).gameObject);
    }
  }

  private static void _Remove(GameObject g) {
    Destroy(g);
  }
}
