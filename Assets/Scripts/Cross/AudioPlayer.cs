using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
  public AudioClip clip;
  public bool setAtStart;
  public bool repeat;
  AudioSource player;

  void Awake() {
    player = GetComponent<AudioSource>();
  }

  void Start() {
    if (setAtStart) {
      SetAudio();
    }
  }

  public void SetAudio() {
    player.clip = clip;
    player.loop = repeat;
    player.Play();
  }
}