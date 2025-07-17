using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;
    public bool isDead = false;

    private Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private AudioSource[] audioSources;

    [Header("UI")]
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();

        // Update UI
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("ZombieHealth: No healthBarSlider assigned.");
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Zombie died!");

        if (agent != null)
            agent.enabled = false;

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }

    }
}
