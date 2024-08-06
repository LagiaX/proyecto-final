using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

  [Header("AI")]
  public GameObject Target;
  public Vector3[] checkPoints;
  public Vector3 currentPoint;
  public SphereCollider collisionDetection, collisionAttack;
  public bool isAttacking;

  [Header("Info")]
  public int index; // current target check point

  [Header("Options")]
  public bool randomRoute;
  public int damage;
  public float detectionRange, attackRange;
  public float walkingSpeed, chasingSpeed;
  public float idleTime;

  private NavMeshAgent agent;
  private Animator animator;
  private float timer;

  void Awake() {
    agent = GetComponent<NavMeshAgent>();
    animator = transform.GetComponent<Animator>();
  }

  void Start() {
    //collisionDetection.transform.localScale = Vector3.one * detectionRange * 2;
    //collisionAttack.transform.localScale = Vector3.one * attackRange * 2;
    agent.speed = walkingSpeed;
  }

  void Update() {
    Behavior();
  }

  public void Behavior() {
    if (isAttacking) return;
    if (Target == null) {
      Patrol();
      return;
    }
    Chase();
  }

  public void Chase() {
    agent.destination = Target.transform.position;
    isAttacking = false;
  }

  public void Patrol() {
    isAttacking = false;
    if (timer > 0) {
      timer -= Time.deltaTime;
      return;
    }
    agent.isStopped = false;
    animator.SetBool("isIdle", false);
    agent.destination = currentPoint;
    CheckDistance();
  }

  public void CheckDistance() {
    bool reachedPointHorizontally = Vector3.Distance(transform.position, currentPoint) < 10f;
    Vector3 checkpointNoHeight = new Vector3(currentPoint.x, transform.position.y, currentPoint.z);
    bool reachedPoint = Vector3.Distance(transform.position, checkpointNoHeight) < 0.2f;
    if (reachedPointHorizontally && reachedPoint) {
      timer = idleTime;
      agent.isStopped = true;
      animator.SetBool("isIdle", true);
      ChangeCheckPoint();
    }
  }

  public void ChangeCheckPoint() {
    agent.speed = walkingSpeed;
    if (randomRoute) {
      index = Random.Range(0, checkPoints.Length);
    }
    else {
      index = (index + 1) % checkPoints.Length;
    }
    currentPoint = checkPoints[index];
  }

  //public void DefineTarget(GameObject target) {
  //  agent.speed = chasingSpeed;
  //  Target = target;
  //}

  //public void Attack(GameObject target) {
  //  agent.isStopped = true;
  //  animator.SetBool("isAttacking", true);
  //  IHasLife damagable = target.GetComponent<IHasLife>();
  //  if (damagable != null) {
  //    damagable.ModifyMissingHealth(damage);
  //    target = null;
  //  }
  //}
}
