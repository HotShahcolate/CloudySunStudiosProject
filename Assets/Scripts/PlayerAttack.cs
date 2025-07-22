using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 1f;
    public float attackDamagePowerUp = 1f;
    public AudioSource swingSound;
    //public Animator animator;
    public KeyCode attackKey = KeyCode.F;
    public KeyCode BlockKey = KeyCode.Space;

    public Animator animator;
    public string swingTriggerName = "slash";
    public string blockTriggerName = "block";

    private float blockBlendVelocity = 0f;
    public bool isCountering = false;
    public GameObject blockPowerUpPanel;
    private bool hasShownBlockPowerUp = false;
    void Start()
    {
        if (blockPowerUpPanel != null)
        {
            blockPowerUpPanel.SetActive(false);
        }
    }

    void Update()
    {
        isCountering = Input.GetKey(BlockKey) || Input.GetMouseButton(1);

        if (Input.GetKeyDown(attackKey) || Input.GetMouseButtonDown(0))
        {
            // 1. Trigger attack animation
            if (animator != null)
            {
                Debug.Log("Swinging Triggered!");
                animator.ResetTrigger(swingTriggerName);
                animator.SetTrigger(swingTriggerName);
            }
            else
            {
                Debug.Log("Animator not found!");

            }

        }

        CharacterControl characterControl = GetComponent<CharacterControl>();
        if (characterControl.chestCount >= 2)
        {
            ShowBlockPowerUp();
            if (Input.GetKeyDown(BlockKey) || Input.GetMouseButtonDown(1))
            {
                // 1. Trigger attack animation
                if (animator != null)
                {
                    //isCountering = true;    
                    Debug.Log("Blocking Triggered!");
                    animator.SetBool("block", isCountering);

                }
                else
                {
                    Debug.Log("Animator not found!");

                }

            }
            else
            {
                animator.SetBool("block", isCountering);
            }
        }

        float targetBlend = isCountering ? 1.0f : 0.0f;
        float currentBlend = animator.GetFloat("Blocking");
        float smoothBlend = Mathf.SmoothDamp(currentBlend, targetBlend, ref blockBlendVelocity, 0.1f);
        animator.SetFloat("Blocking", smoothBlend);
    }

    public void PerformAttack()
    {
        SwordCollector collector = GetComponent<SwordCollector>();

        if (collector != null)
        {
            Debug.Log("Script not found!");
        }

        if (collector.PowerUp)
        {
            Attack(attackDamage + 2f, swingSound);
        }
        else
        {
            Attack(attackDamage, swingSound);
        }




    }

    public void Attack(float attackDam, AudioSource swing)
    {
        // 2. Play swing sound
        if (swing != null)
            swing.Play();

        // 3. Perform raycast to hit zombie
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, attackRange))
        if (Physics.SphereCast(ray, 0.3f, out hit, attackRange))
        {
            var zombie = hit.collider.GetComponentInParent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.TakeDamage(attackDam);
                Debug.Log("Zombie hit!");
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void ShowBlockPowerUp()
{
    CharacterControl characterControl = GetComponent<CharacterControl>();

    if (!hasShownBlockPowerUp && characterControl != null && characterControl.chestCount >= 2 && blockPowerUpPanel != null)
    {
        blockPowerUpPanel.SetActive(true);
        Invoke("HideBlockPowerUpPanel", 2f);
        hasShownBlockPowerUp = true; 
    }
}
    void HideBlockPowerUpPanel()
    {
        if (blockPowerUpPanel != null)
        {
            blockPowerUpPanel.SetActive(false);
           
        }
    }
    
    
}
