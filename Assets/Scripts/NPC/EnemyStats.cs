using UnityEngine;

public class EnemyStats : OrganicTarget {
  public StatsNpc stats;
  public EnemyType enemyType;

  protected override void Start() {
    InitStats();
  }

  public void InitStats() {
    health = Utils.GetNPCBaseHealth(enemyType);
    stats = Utils.GetNPCBaseStats();
  }
}
