using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieChase : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 1.0f;
    public float attackCooldown = 1.0f;
    public float damage = 1f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = -999f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isAttacking", false);

            // Check for attack range
            if (distance <= attackRange && Time.time - lastAttackTime > attackCooldown)
            {
                // Attack
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastAttackTime = Time.time;
                    //animator.SetTrigger("Attack"); // Optional animation trigger
                    Debug.Log("Zombie attacks the player!");
                }
            }
        }
        else
        {
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetBool("isAttacking", false);
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
