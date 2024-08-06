
public class PowerUpSpeed : PowerUp {

  public float modifier;

  public override void OnCollect(AliveTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
      playerStats.stats.ModifyMovementSpeed(modifier);
      base.OnCollect(collector);
    }
  }
}