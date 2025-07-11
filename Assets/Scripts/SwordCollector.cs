using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    public Transform swordHold;

    public Rigidbody swordPrefab;

    private Animator animator;

    private Rigidbody currSword;
    private bool doSlash = false;

    private float slashTimer = 0f;
    public float slashDuration = .20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currSword != null && Input.GetKeyDown(KeyCode.F))
        {
            doSlash = true;
            //Debug.Log("Fire1 presssed");
            //animator.SetTrigger("slash");
            //animator.SetBool("slash", false);
        }
    }

    public void RecieveSword()
    {
        if (currSword != null)
        {
            Debug.LogWarning("Has sword already");
        }

        currSword = Instantiate(swordPrefab, swordHold);

        //Align the possition
        currSword.transform.localPosition = Vector3.zero;
        currSword.transform.localRotation = Quaternion.identity;

        //Set Kinematic to true
        currSword.isKinematic = true;
    }

    private void FixedUpdate()
    {
        // Reset the throw bool so it's "sticky" — lasts only one frame
        if (doSlash)
        {
            animator.SetBool("slash", true);
            slashTimer = 0f;
            //Debug.Log("Set bool to true");
            doSlash = false;
        }
        else
        {
            slashTimer += Time.fixedDeltaTime;
            //Debug.Log($"Throw timer: {throwTimer}");
            if (slashTimer >= slashDuration)
            {
                animator.SetBool("slash", false);
                slashTimer = 0f;
            }
        }
    }

        void Awake()
    {
        animator = GetComponent<Animator>();
        swordHold = this.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/SwordPlacement");

        if (swordHold == null )
        {
            Debug.LogError("swordHold could not find");
        }
    }
}
