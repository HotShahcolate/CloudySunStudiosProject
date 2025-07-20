using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 10f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Respawn")]
    public RespawnPlayer respawnScript;

    [Header("UI References")]
    public Slider healthBar;
    public GameObject playerDeadPanel;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (playerDeadPanel != null)
        {
            playerDeadPanel.SetActive(false); 
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        // 1. Trigger attacked animation
        if (animator != null)
        {
            Debug.Log("Attacked Triggered!");
            //animator.ResetTrigger("attacked");
            animator.SetTrigger("attacked");
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //animator.ResetTrigger("attacked");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void AddHealth(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        //animator.ResetTrigger("attacked");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died!");

        if (playerDeadPanel != null)
        {
            playerDeadPanel.SetActive(true); 
        }

        Invoke(nameof(Respawn), 1.5f); 
    }

    void Respawn()
    {
        respawnScript.Respawn();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (playerDeadPanel != null)
        {
            playerDeadPanel.SetActive(false);
        }

        isDead = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
