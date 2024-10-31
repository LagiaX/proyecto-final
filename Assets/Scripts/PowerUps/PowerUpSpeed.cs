using UnityEngine;

public class PowerUpSpeed : PowerUp {

  public float modifier;
  public float duration;
  public AudioSource speedSFX;

  public override void OnCollect(OrganicTarget collector) {
    if (collector.TryGetComponent(out PlayerStats playerStats)) {
      speedSFX.Play();
      playerStats.OnNewBuff(Buff.Speed, modifier, duration);
      base.OnCollect(collector);
    }
  }
}
