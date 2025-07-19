using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieChase : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 2.0f;
    public float attackCooldown = 1.0f;
    public float damage = 1f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = -999f;

    private ZombieHealth zombieHealth;

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
    }

    void Update()
    {
        if (zombieHealth != null && zombieHealth.isDead)
            return;

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            Debug.Log("Zombie is chasing player!");
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
                    Debug.Log("Zombie attacks the player!");
                }
            }
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
