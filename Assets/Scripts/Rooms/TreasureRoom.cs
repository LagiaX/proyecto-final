using System.Collections;
using UnityEngine;

public class TreasureRoom : MonoBehaviour {

  public static event GameManager.PauseGame Pause;

  public AudioSource bgm;
  public CanvasRenderer score;
  public CanvasRenderer retryMenu;

  public float fadeTime;

  private bool _triggered;
  private AudioSource _jingle;

  void Awake() {
    _jingle = GetComponent<AudioSource>();
  }

  public void OnTriggerEnter(Collider other) {
    if (!_triggered && other.gameObject.TryGetComponent(out PlayerStats p)) {
      _triggered = true;
      StartCoroutine(_FadeBGM());
      _jingle.Play();
      StartCoroutine(_GoalAnimation());
    }
  }

  private IEnumerator _GoalAnimation() {
    Time.timeScale = 0.3f;
    yield return new WaitUntil(() => _jingle.isPlaying == false);
    // deactivate controls
    Pause?.Invoke(!GameManager.isPaused);
    // show score HUD and options
    score.gameObject.SetActive(true);
    retryMenu.gameObject.SetActive(true);
  }

  private IEnumerator _FadeBGM() {
    float startVolume = bgm.volume;
    while (bgm.volume > 0) {
      bgm.volume -= startVolume * Time.deltaTime / fadeTime;
      yield return null;
    }
    bgm.Stop();
  }
}
