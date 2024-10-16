
public class PowerUpSpeed : PowerUp {

  public float modifier;
  public float duration;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
      playerStats.OnNewBuff(Buff.Speed, modifier, duration);
      base.OnCollect(collector);
    }
  }
}
