
public class PowerUpHealth : PowerUp {

  public int recovery;
  public bool isInverseRecovery;

  public override void OnCollect(AliveTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
      base.OnCollect(collector);
      if (isInverseRecovery) {
        collector.OnDamage(recovery);
        return;
      }
      playerStats.health.ModifyMissingHealth(-recovery);
    }
  }
}