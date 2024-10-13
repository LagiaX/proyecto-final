using System.Threading.Tasks;
using UnityEngine;

public class ActivatableTotem : MonoBehaviour, IActivatable {

  public Color initialColor;
  public Color finalColor;
  public float intensity;
  public float intensityLimit;
  public float maxTime;
  public float inc;
  public new Renderer renderer;

  private Material _material;

  void Awake() {
    _material = renderer.material;
    inc = intensityLimit / maxTime;
  }

  public void OnActivate() {
    _material.SetColor(Shortcuts.Emission, finalColor);
  }

  public void Deactivate() {
    _material.SetColor(Shortcuts.Emission, initialColor);
  }

  // unused code, doesn't work as is
  public async void ManaAnimation() {
    while (intensity < intensityLimit) {
      intensity += inc * Time.deltaTime;
      intensity = Mathf.Clamp(intensity, 0, intensityLimit);
      _material.SetColor(Shortcuts.Emission, Color.Lerp(initialColor, finalColor, intensity / intensityLimit));
      await Task.Delay(10);
    }
  }
}
