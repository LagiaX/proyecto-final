using UnityEngine;

public class PowerUpHealth : PowerUp {

  public int recovery;
  public bool isInverseRecovery;
  public AudioSource healthSFX;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
      healthSFX.Play();
      base.OnCollect(collector);
      if (isInverseRecovery) {
        collector.OnDamage(recovery);
        return;
      }
      playerStats.OnCureAilment(Ailment.Poisoned);
      playerStats.OnCureAilment(Ailment.Burned);
      playerStats.OnRestoreHealth(recovery);
    }
  }
}
