
public class PowerUpHealth : PowerUp {

  public int recovery;
  public bool isInverseRecovery;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
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
