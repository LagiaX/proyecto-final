using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
  [Header("AI")]
  public Transform chasingTarget;
  public Transform[] patrolCheckpoints;
  public Vector3 patrolDestination;

  [Header("Info")]
  public DetectionRange detectionTrigger;

  [Header("Options")]
  public bool randomRoute;
  public int damage;
  public float detectionRange;
  public float patrolSpeed;
  public float chasingSpeed;
  public float idleTime;

  private NavMeshAgent _agent;
  private int _patrolCheckpointIndex; // current patrol destination
  private float _idleTimer;

  void Awake() {
    if (detectionTrigger == null)
      Utils.MissingComponent(typeof(DetectionRange).Name, name);
    if (!TryGetComponent(out _agent))
      Utils.MissingComponent(typeof(NavMeshAgent).Name, name);
  }


  void OnEnable() {
    DebugManager.DebugMode += _DebugMode;
    detectionTrigger.Detected += ChaseTarget;
    detectionTrigger.TargetLost += ResetTarget;
  }

  void OnDisable() {
    DebugManager.DebugMode -= _DebugMode;
    detectionTrigger.Detected -= ChaseTarget;
    detectionTrigger.TargetLost -= ResetTarget;
  }

  void Start() {
    damage = AppConfig.EnemyContactDamage;
    detectionTrigger.transform.localScale = Vector3.one * detectionRange;
    if (HasPatrolBehavior()) {
      patrolDestination = patrolCheckpoints[0].position;
    }
  }

  void Update() {
    if (_agent.isOnNavMesh) {
      _Behavior();
    }
  }

  private void _DebugMode(bool option) {
    detectionTrigger.GetComponent<Renderer>().enabled = option;
  }

  private void _Behavior() {
    if (chasingTarget != null) {
      Chase();
      return;
    }
    if (HasPatrolBehavior()) {
      Patrol();
    }
  }

  public void Chase() {
    _agent.SetDestination(chasingTarget.position);
    _agent.speed = chasingSpeed;
  }

  public void Patrol() {
    if (_idleTimer > 0) {
      _idleTimer -= Time.deltaTime;
      return;
    }
    _agent.SetDestination(patrolDestination);
    _agent.speed = patrolSpeed;
    if (IsPointReachedHorizontally()) {
      _idleTimer = idleTime;
      ChangeCheckPoint();
    }
  }

  public bool HasPatrolBehavior() {
    return patrolCheckpoints.Length > 0;
  }

  public bool IsPointReachedHorizontally() {
    // create a checkpoint at the same VERTICAL position but with the checkpoint's HORIZONTAL position
    // then compare it to the current position
    Vector3 checkpointNoHeight = new Vector3(patrolDestination.x, transform.position.y, patrolDestination.z);
    return Vector3.Distance(transform.position, checkpointNoHeight) < 0.2f;
  }

  public bool IsPointReachedVertically() {
    // create a checkpoint at the same HORIZONTAL position but with the checkpoint's VERTICAL position
    // then compare it to the current position
    Vector3 checkpointOnlyHeight = new Vector3(transform.position.x, patrolDestination.y, transform.position.z);
    return Vector3.Distance(transform.position, checkpointOnlyHeight) < 0.2f;
  }

  public void ChangeCheckPoint() {
    if (randomRoute) {
      _patrolCheckpointIndex = Random.Range(0, patrolCheckpoints.Length);
    }
    else {
      _patrolCheckpointIndex = (_patrolCheckpointIndex + 1) % patrolCheckpoints.Length;
    }
    patrolDestination = patrolCheckpoints[_patrolCheckpointIndex].position;
  }

  public void ChaseTarget(Transform target) {
    chasingTarget = target;
  }

  public void ResetTarget(Transform target) {
    if (chasingTarget == target) {
      chasingTarget = null;
    }
  }

  //public void IsStuck() {
  //  if (_agent.speed < 0.2f) {
  //    _stayTimer += Time.deltaTime;
  //    if (_stayTimer >= maxStayTime) {
  //      DefineTarget(null);
  //      ChangeCheckPoint();
  //    }
  //  }
  //}

  //public void CheckStayTime() {
  //  if (_agent.speed <= 0.5) {
  //    _stayTimer += Time.deltaTime;
  //  }
  //  else {
  //    _stayTimer = 0;
  //  }
  //  if (_stayTimer >= maxStayTime) {
  //    DefineTarget(null);
  //    ChangeCheckPoint();
  //    _stayTimer = 0;
  //  }
  //}

  //public void Attack(Transform attackingTarget) {
  //  _agent.isStopped = _isAttacking = true;
  //  //_animator.SetTrigger("OnAttack");
  //  IHasLife damagable;
  //  if (!attackingTarget.TryGetComponent<IHasLife>(out damagable)) {
  //    damagable.ModifyMissingHealth(damage);
  //    chasingTarget = null;
  //  }
  //}
}
