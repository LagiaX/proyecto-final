using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class HeadUpDisplay : MonoBehaviour {
  public static HeadUpDisplay instance;

  [Header("Health")]
  public int totalHearts;
  public Image[] heartIcons;
  public Image[] emptyHeartIcons;
  public RectTransform healthBG;

  [Header("Weapons")]
  public Image[] weapons;
  public Sprite[] weaponSprites;

  [Header("Coins")]
  public TMP_Text coins;

  [Header("Mana Circuits")]
  public Image[] circuits;
  public Sprite circuitObtained;
  public Sprite circuitUnobtained;

  [Header("Controls")]
  public TMP_Text controls;

  private int _currentWeapon = 0;
  private int _currentHeartIcon;

  void Awake() {
    if (instance == null) {
      instance = this;
      return;
    }
    Destroy(gameObject);
  }

  void OnEnable() {
    ButtonConfiguration.ControlChanged += _UpdateControls;
    PlayerStats.PlayerAlive += InitHealth;
    SpawnManager.PlayerRespawn += InitHealth;
    PlayerStats.HealthChanged += OnHealthChange;
    PlayerStats.PlayerHealed += OnPlayerHeal;
    PlayerStats.PlayerDamaged += OnPlayerDamage;
    PlayerInventory.InventoryChanged += OnInventoryChange;
    PlayerInventory.CoinCollect += OnCoinCollect;
    PlayerInventory.CircuitCollect += OnCircuitCollect;
    PlayerInventory.WeaponCollect += OnWeaponCollect;
    PlayerInventory.WeaponChange += OnWeaponChange;
  }

  void OnDisable() {
    PlayerStats.PlayerAlive -= InitHealth;
    SpawnManager.PlayerRespawn -= InitHealth;
    PlayerStats.HealthChanged -= OnHealthChange;
    PlayerStats.PlayerHealed -= OnPlayerHeal;
    PlayerStats.PlayerDamaged -= OnPlayerDamage;
    PlayerInventory.InventoryChanged -= OnInventoryChange;
    PlayerInventory.CoinCollect -= OnCoinCollect;
    PlayerInventory.CircuitCollect -= OnCircuitCollect;
    PlayerInventory.WeaponCollect -= OnWeaponCollect;
    PlayerInventory.WeaponChange -= OnWeaponChange;
  }

  void Start() {
    _UpdateControls(AppConfig.Control.Jump);
  }

  private void _UpdateControls(AppConfig.Control c) {
    string text = "Movement - WASD" +
      "\nJump - " + AppConfig.KeyBindings[AppConfig.Control.Jump] +
      "\nShoot - " + AppConfig.KeyBindings[AppConfig.Control.Shoot] +
      "\nChange Weapon - " + AppConfig.KeyBindings[AppConfig.Control.ChangeWeapon] +
      "\nLock On - " + AppConfig.KeyBindings[AppConfig.Control.LockTarget];
    controls.text = text;
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

  public void OnHealthChange(Health h) {
    _UpdateHealthIndicator(h.healthCurrent / 4, h.healthCurrent % 4);
  }

  public void OnInventoryChange(Inventory i) {
    OnCoinCollect(i.coins);
    for (int index = 0; index < i.manaCircuits.Length; index++) {
      OnCircuitCollect(i.manaCircuits[index].collected, i.manaCircuits[index].id);
    }
    for (int index = 0; index < i.weapons.Length; index++) {
      OnWeaponCollect(i.weapons[index], index);
    }
  }

  public void OnCoinCollect(int value) {
    int digits = Inventory.COINS_MAX.ToString().Length;
    string adjustment = "";
    for (int i = 0; i < digits; i++) {
      adjustment += 0;
    }
    coins.text = string.Format("{0:" + adjustment + "}", value);
  }

  public void OnCircuitCollect(bool obtained, int id) {
    // TODO: Mana circuits HUD indicators
    Image icon = circuits[id];
    icon.sprite = obtained ? circuitObtained : circuitUnobtained;
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

  public void OnWeaponCollect(WeaponType type, int slot) {
    Image icon = weapons[slot];
    icon.sprite = weaponSprites[(int)type];
    if (_currentWeapon != slot) {
      icon.color = new Vector4(icon.color.r, icon.color.g, icon.color.b, 0.5f);
    }
  }

  public void OnWeaponChange(int slot) {
    Image currentIcon = weapons[_currentWeapon];
    weapons[_currentWeapon].color = new Vector4(currentIcon.color.r, currentIcon.color.g, currentIcon.color.b, 0.5f);
    weapons[slot].color = new Vector4(currentIcon.color.r, currentIcon.color.g, currentIcon.color.b, 1f);
    _currentWeapon = slot;
  }
}
