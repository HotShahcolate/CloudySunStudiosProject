using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieControl : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 2.0f;
    public float attackCooldown = 1.0f;
    public float damage = 1f;

    public Vector3 wanderCenter;
    public float wanderRadius = 5f;
    public float wanderInterval = 3f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = -999f;

    private ZombieHealth zombieHealth;

    private float nextWanderTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        zombieHealth = GetComponent<ZombieHealth>();

        agent.stoppingDistance = attackRange;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        wanderCenter = transform.position;
    }

    void Update()
    {
        if (zombieHealth != null && zombieHealth.isDead)
            return;

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            ChasePlayer(distance);
            Debug.Log("Zombie is chasing player!");
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        if (Time.time >= nextWanderTime)
        {
            Vector3 randomPos = wanderCenter + Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 2f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                animator.SetBool("isWalking", true);
            }
            nextWanderTime = Time.time + wanderInterval;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void ChasePlayer(float distance)
    {
        bool isInAttackRange = distance <= attackRange;

        if (isInAttackRange)
        {
            agent.isStopped = true;
            agent.ResetPath();
            FacePlayer();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        animator.SetBool("isRunning", !agent.isStopped);
        animator.SetBool("isAttacking", isInAttackRange);

        if (isInAttackRange && Time.time - lastAttackTime > attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
