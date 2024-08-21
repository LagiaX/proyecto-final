using System.Linq;
using UnityEditor;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
  public delegate void ColorChanged(Color c);
  public static event ColorChanged ColorChange;

  private Renderer _renderer;
  private Color[] _colors;
  private int _colorIndex = 0;

  void Awake() {
    if (!TryGetComponent(out _renderer)) {
      Utils.MissingComponent(typeof(Renderer).Name, name);
    }
    _colors = new Color[3]{ Color.red, Color.green, Color.blue };
    _colorIndex = ArrayUtility.FindIndex(_colors, (c) => c == _renderer.material.color);
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.TryGetComponent(out PlayerSystems player)) {
      _colorIndex = (_colorIndex + 1) % _colors.Length;
      _renderer.material.color = _colors[_colorIndex];
      ColorChange?.Invoke(_renderer.material.color);
    }
  }
}
