using UnityEditor;
using UnityEngine;

public class ColorChanger : MonoBehaviour, IActivatable {
  public delegate void ColorChanged(Color c);
  public event ColorChanged ColorChange;

  public Material[] materials;

  private Renderer _renderer;
  private int _materialIndex = 0;
  private bool _canChangeColor = true;

  void Awake() {
    if (!TryGetComponent(out _renderer)) {
      Utils.MissingComponent(typeof(Renderer).Name, name);
    }

    _materialIndex = ArrayUtility.FindIndex(materials, (m) => m.color == _renderer.material.color);
  }

  public void CanChangeColor(bool canChange) {
    _canChangeColor = canChange;
  }

  public void OnActivate() {
    if (_canChangeColor) {
      _materialIndex = (_materialIndex + 1) % materials.Length;
      _renderer.material = materials[_materialIndex];
      ColorChange?.Invoke(_renderer.material.color);
    }
  }
}
