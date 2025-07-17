using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class ZombieControl : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 3.0f;
    public float attackCooldown = 1.0f;
    public float damage = 1f;

    public Vector3 wanderCenter;
    public float wanderRadius = 5f;
    public float wanderInterval = 3f;

    public AudioClip wanderClip;
    public AudioClip runClip;
    public AudioClip attackClip;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = -999f;

    private ZombieHealth zombieHealth;

    private float nextWanderTime = 0f;

    private AudioSource audioSource;
    private string currentAudioState = "";


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        zombieHealth = GetComponent<ZombieHealth>();
        audioSource = GetComponent<AudioSource>();

        agent.stoppingDistance = attackRange;
        agent.isStopped = true;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Set up 3D audio behavior
        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = detectionRadius * 1.5f;

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
        {
            StopAudio();
            return;
        }

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

        if (Time.time >= nextWanderTime || !agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randomPos = wanderCenter + Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 2f, NavMesh.AllAreas))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.position);
                UpdateAudio("Wander");
            }
            nextWanderTime = Time.time + wanderInterval;
        }

        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);
        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
    }

    void ChasePlayer(float distance)
    {
        bool isInAttackRange = distance <= attackRange;

        if (isInAttackRange)
        {
            agent.isStopped = true;
            agent.ResetPath();
            FacePlayer();
            UpdateAudio("Attack");
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            UpdateAudio("Run");
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

    void UpdateAudio(string state)
    {
        if (currentAudioState == state) return;

        switch (state)
        {
            case "Wander":
                audioSource.clip = wanderClip;
                break;
            case "Run":
                audioSource.clip = runClip;
                break;
            case "Attack":
                audioSource.clip = attackClip;
                break;
            default:
                StopAudio();
                return;
        }

        audioSource.Play();
        currentAudioState = state;
    }

    void StopAudio()
    {
        audioSource.Stop();
        currentAudioState = "";
    }

}
