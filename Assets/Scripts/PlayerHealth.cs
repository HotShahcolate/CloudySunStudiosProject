using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 10f;
    private float currentHealth;
    public float curseLevel;
    private bool isDead = false;

    [Header("Respawn")]
    public RespawnPlayer respawnScript;

    [Header("UI References")]
    public Slider healthBar;
    public Slider curseBar;
    public GameObject playerDeadPanel;
    public Animator animator;
    public GameObject curseText;

    public Coroutine damageCoroutine = null;

    void Start()
    {
        curseText.SetActive(false);
        currentHealth = maxHealth;
        curseLevel = 0;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (curseBar != null)
        {
            curseBar.maxValue = 10;
            curseBar.value = curseLevel;
        }

        if (playerDeadPanel != null)
        {
            playerDeadPanel.SetActive(false); 
        }
    }

    public void TakeCurse()
    {
        if (isDead) return;

        curseLevel++;

        if (curseBar != null)
        {
            curseBar.value = curseLevel;
        }

        if (curseLevel >= 10)
        {
            curseText.SetActive(true);
            damageCoroutine = StartCoroutine(TakeDamageRepeat());
        }
    }

    IEnumerator TakeDamageRepeat()
    {
        while (currentHealth > 0)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(2);
        }
    }

    public void CureCurse()
    {
        print("this is running");

        curseLevel = 0;

        if (curseBar != null)
        {
            curseBar.value = curseLevel;
        }
        StopCoroutine(damageCoroutine);
        curseText.SetActive(false);
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

        CureCurse();

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
