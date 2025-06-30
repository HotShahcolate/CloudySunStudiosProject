using UnityEngine;

public class HealthPackFloat : MonoBehaviour
{
    [Header("Animation")]
    private Animator animator;

    [Header("Health Settings")]
    public float healAmount = 5f;

    [Header("Audio")]
    public AudioSource audioSource;

    private bool collected = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered trigger: " + other.name);
            animator.SetBool("PlayerNearby", true);

            if (animator != null)
                animator.SetBool("PlayerNearby", true);

            // heal player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(-healAmount);
                Debug.Log("Healed player for " + healAmount);

                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                collected = true;

                // destroy after the sound plays
                float delay = (audioSource != null && audioSource.clip != null) ? audioSource.clip.length : 0.5f;

                Destroy(gameObject, delay); // remove health pack
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            if (animator != null)
            {
                animator.SetBool("PlayerNearby", false);
                Debug.Log("PlayerNearby = false");
            }
        }
    }
}
