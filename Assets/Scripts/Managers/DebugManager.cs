using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour {
  public static DebugManager instance;

  public delegate void Activate(bool option);
  public static event Activate DebugMode;

  public bool isDebug;
  public TMP_Text textFps;
  public float fps;

  private float _fpsCumulative;
  private float _timer;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void Start() {
    DebugMode += DisplayFPS;
    DebugMode?.Invoke(isDebug);
  }

  void Update() {
    _fpsCounter();
  }

  private void _fpsCounter() {
    _timer += Time.deltaTime;
    _fpsCumulative += 1;
    if (_timer >= 0.5f) {
      fps = _fpsCumulative / _timer;
      _timer = 0;
      _fpsCumulative = 0;
      textFps.text = (int)fps + "FPS";
    }
  }

  public void DisplayFPS(bool option) {
    textFps.transform.parent.gameObject.SetActive(isDebug);
  }
}
