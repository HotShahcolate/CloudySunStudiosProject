using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class SwordAnimation : MonoBehaviour
{
    public float liftDuration = 1f;
    public AudioSource audioSource;
    private float timer = 0f;

    private bool lifted = false;
    private bool triggered;
    private bool notTriggered = false;
    private bool isPlayerNear = false;
    private GameObject player;
    private Collider Collider;

    //private Rigidbody rb;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.LeftControl))
        {
            SwordCollector collector = Collider.GetComponent<SwordCollector>();
            if (collector != null)
            {
                collector.RecieveSword();

                //if (audioSource != null && !audioSource.isPlaying)
                //{
                //    audioSource.Play();
                //}

                //float delay = (audioSource != null && audioSource.clip != null) ? audioSource.clip.length : 0.5f;

                Destroy(this.gameObject);
            }
        }

    }

    void FixedUpdate()
    {
        
        if (triggered)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / liftDuration);

            anim.SetBool("IsRaising", true);
            //Debug.Log("progress - " + progress);

            if (progress >= 1f)
            {
                triggered = false;
                lifted = true;
                //Debug.Log("IsRaisied, true");
                //Debug.Log("IsRaising, false");
            }


        }
        else if (notTriggered)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / liftDuration);

            anim.SetBool("IsLowering", true);
            anim.SetBool("IsRaising", false);
            anim.SetBool("IsSpinning", false);
            anim.SetBool("IsRaised", false);

            if (progress >= 1f)
            {
                notTriggered = false;
                lifted = false;
                //rb.isKinematic = false;
                //rb.useGravity = true;
                anim.SetBool("IsLowering", false);
            }

        }
        else if (lifted)
        {
            anim.SetBool("IsRaised", true);
            anim.SetBool("IsRaising", false);
            anim.SetBool("IsSpinning", true);

        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator could not be found");
        }
        else
        {
            Debug.Log("Animator found");
        }

        //if (audioSource == null)
        //{
        //    audioSource = GetComponent<AudioSource>();
        //}
    }




    private void OnTriggerEnter(Collider c)
    {
        

        // Trigger if the incoming object has a Rigidbody (e.g., a player or AI character)
        if (!triggered && c.CompareTag("Player"))
        {
            //rb.isKinematic = true;
            //rb.useGravity = false;
            //Debug.Log("Tiggered");

            timer = 0f;
            triggered = true;
            notTriggered = false;
            isPlayerNear = true;
            Collider = c;
            //Debug.Log("OnTriggerEnter running - triggered: " + triggered);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            timer = 0f;
            triggered = false;
            notTriggered = true;
            isPlayerNear = false;
            //Debug.Log("OnTriggerExit running - triggered: " + triggered);
        }
    }
}
