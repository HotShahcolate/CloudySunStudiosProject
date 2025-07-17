using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieChase : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 2.0f;

    public float attackCooldownMin = 0.8f;
    public float attackCooldownMax = 1.5f;
    public float damage = 1f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float nextAttackTime = 0f;    //private float lastAttackTime = -999f;

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

                animator.SetBool("isAttacking", true);
                animator.SetFloat("MoveSpeed", 0f);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);

                animator.SetBool("isAttacking", false);
                animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
            }

            if (isInAttackRange && Time.time >= nextAttackTime)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    nextAttackTime = Time.time + Random.Range(attackCooldownMin, attackCooldownMax);
                    Debug.Log("Zombie attacks the player!");
                }
            }
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;

            animator.SetBool("isAttacking", false);
            animator.SetFloat("MoveSpeed", 0f);
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
