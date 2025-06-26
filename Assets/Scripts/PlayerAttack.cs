using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 1f;
    public AudioSource swingSound;
    //public Animator animator;
    public KeyCode attackKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            // 1. Trigger attack animation
            /*if (animator != null)
                animator.SetTrigger("Attack");*/

            // 2. Play swing sound
            if (swingSound != null)
                swingSound.Play();

            // 3. Perform raycast to hit zombie
            Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, attackRange))
            {
                var zombie = hit.collider.GetComponentInParent<ZombieHealth>();
                if (zombie != null)
                {
                    zombie.TakeDamage(attackDamage);
                    Debug.Log("Zombie hit!");
                }
            }
        }
    }
}
