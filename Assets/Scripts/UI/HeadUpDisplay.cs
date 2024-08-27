using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HeadUpDisplay : MonoBehaviour {
  public static HeadUpDisplay instance;

  public int totalHearts;
  public Image[] heartIcons;
  public Image[] emptyHeartIcons;
  public RectTransform healthBG;

  public TMP_Text coins;

  private int _currentHeartIcon;

  void Awake() {
    if (instance == null) {
      instance = this;
      PlayerStats.PlayerAlive += InitHealth;
      SpawnManager.PlayerRespawn += InitHealth;
      PlayerStats.PlayerHealed += OnPlayerHeal;
      PlayerStats.PlayerDamaged += OnPlayerDamage;
      return;
    }
    Destroy(gameObject);
  }

  private void _UpdateHealthIndicator(int filledHearts, int heartParts) {
    for (int i = 0; i < totalHearts; i++) {
      if (i < filledHearts) {
        heartIcons[i].fillAmount = 1;
        _currentHeartIcon = i + 1;
      }
      else {
        heartIcons[i].fillAmount = 0;
      }
    }

    if (filledHearts == 0) { _currentHeartIcon = 0; }
    else if (filledHearts == totalHearts) { _currentHeartIcon = totalHearts - 1; }

    if (heartParts > 0) {
      heartIcons[_currentHeartIcon].fillAmount = heartParts * 0.25f;
    }
  }

  public void InitHealth(PlayerStats ps) {
    totalHearts = ps.health.healthMax / 4;
    healthBG.sizeDelta = new Vector2(AppConfig.SpaceBetweenHeartIcons * (totalHearts + 1) + totalHearts * 50, 100);
    for (int i = 0; i < totalHearts; i++) {
      heartIcons[i].gameObject.SetActive(true);
      emptyHeartIcons[i].gameObject.SetActive(true);
    }
    _UpdateHealthIndicator(ps.health.healthCurrent / 4, ps.health.healthCurrent % 4);
  }

  public void ModifyCoins(int value) {
    int digits = Inventory.COINS_MAX.ToString().Length;
    string adjustment = "";
    for (int i = 0; i < digits; i++) {
      adjustment += 0;
    }
    coins.text = string.Format("{0:" + adjustment + "}", value);
  }

  public void OnPlayerHeal(int healing) {
    // Could be nice to have this for a recovery-over-time effect. This could even be parameterized for use on DOTs, too.
    //float totalHealing = healing;
    //while (totalHealing > 0) {
    //  if (heartIcons[_currentHeartIcon].fillAmount < 1) {
    //    heartIcons[_currentHeartIcon].fillAmount += 0.25f;
    //    totalHealing -= 0.25f;
    //  }
    //}
    _UpdateHealthIndicator(
      PlayerSystems.instance.stats.health.healthCurrent / 4,
      PlayerSystems.instance.stats.health.healthCurrent % 4
    );
  }

  public void OnPlayerDamage(int damage) {
    _UpdateHealthIndicator(
      PlayerSystems.instance.stats.health.healthCurrent / 4,
      PlayerSystems.instance.stats.health.healthCurrent % 4
    );
  }
}
