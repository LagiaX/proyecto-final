using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
  public static AudioManager instance;

  public AudioMixer audioMixer;
  public Slider sliderBGM;
  public Slider sliderSFX;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void Start() {
    sliderBGM.onValueChanged.AddListener((float value) => { SetBGM(value); });
    sliderSFX.onValueChanged.AddListener((float value) => { SetSFX(value); });
    LoadConfig();
  }

  public void SetBGM(float value) {
    value = Mathf.Round(value * 100) / 100;
    audioMixer.SetFloat("BGM_Vol", Mathf.Log(value) * 20);
    AppConfig.SoundSettings.bgmVolume = sliderBGM.value;
  }

  public void SetSFX(float value) {
    value = Mathf.Round(value * 100) / 100;
    audioMixer.SetFloat("SFX_Vol", Mathf.Log(value) * 20);
    AppConfig.SoundSettings.sfxVolume = sliderSFX.value;
  }

  public void LoadConfig() {
    sliderBGM.value = AppConfig.SoundSettings.bgmVolume;
    sliderSFX.value = AppConfig.SoundSettings.sfxVolume;
  }
}
