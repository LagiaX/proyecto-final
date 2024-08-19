using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
  public static AudioManager instance;

  public AudioMixer audioMixer;
  public Slider sliderBGM;
  public Slider sliderSFX;
  public SavedConfig savedConfig;

  public readonly string configRoute = Directory.GetCurrentDirectory() + "/golden.cfg";

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
  }

  public void SetSFX(float value) {
    value = Mathf.Round(value * 100) / 100;
    audioMixer.SetFloat("SFX_Vol", Mathf.Log(value) * 20);
  }

  public void SaveConfig() {
    savedConfig.soundSettings.bgmVolume = sliderBGM.value;
    savedConfig.soundSettings.sfxVolume = sliderSFX.value;
    savedConfig.soundSettings.speakerMode = AudioSettings.GetConfiguration().speakerMode;
    File.WriteAllText(configRoute, JsonUtility.ToJson(savedConfig, true));
  }

  public void LoadConfig() {
    if (!File.Exists(configRoute)) {
      sliderBGM.value = AppConfig.defaultBGMVol;
      sliderSFX.value = AppConfig.defaultSFXVol;
      return;
    }

    savedConfig = JsonUtility.FromJson<SavedConfig>(File.ReadAllText(configRoute));
    sliderBGM.value = savedConfig.soundSettings.bgmVolume;
    sliderSFX.value = savedConfig.soundSettings.sfxVolume;
  }
}
