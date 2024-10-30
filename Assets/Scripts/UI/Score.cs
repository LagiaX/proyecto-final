using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {

  public TMP_Text coins;
  public TMP_Text enemies;
  public TMP_Text circuits;
  public TMP_Text health;
  public TMP_Text totalScore;

  void OnEnable() {
    UpdateScore();
  }

  public void UpdateScore() {
    int scoreCoins = PlayerSystems.instance.inventory.inventory.coins;
    int scoreEnemies = GameManager.enemiesDefeated;
    int scoreCircuits = 0;
    for (int i = 0; i < PlayerSystems.instance.inventory.inventory.manaCircuits.Length; i++) {
      if (PlayerSystems.instance.inventory.inventory.manaCircuits[i].collected) {
        scoreCircuits++;
      }
    }
    int scoreHealth = PlayerSystems.instance.stats.health.healthCurrent;
    coins.text = scoreCoins + " x " + AppConfig.coinBonus;
    enemies.text = scoreEnemies + " x " + AppConfig.enemyBonus;
    circuits.text = scoreCircuits + " x " + AppConfig.circuitBonus;
    health.text = scoreHealth + " x " + AppConfig.healthBonus;
    totalScore.text = (scoreCoins * 100 + scoreEnemies * 750 + scoreCircuits * 1500 + scoreHealth * 500).ToString();
  }
}
