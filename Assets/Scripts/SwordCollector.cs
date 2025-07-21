using System.Collections;
using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    public Transform swordHold;

    public Rigidbody swordPrefab;

    public Rigidbody swordPrefab2;

    private Animator animator;

    private Rigidbody currSword;
    private Coroutine powerUpCoroutine;
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
        //if (currSword != null && Input.GetKeyDown(KeyCode.F))
        //{
        //    doSlash = true;
        //    //Debug.Log("Fire1 presssed");
        //    //animator.SetTrigger("slash");
        //    //animator.SetBool("slash", false);
        //}
    }

    public void RecieveSword()
    {
        if (currSword != null)
        {
            Debug.LogWarning("Already holding a sword.");
            return;
        }

        EquipSword(swordPrefab);
    }

    public void RecievePowerUp(float duration)
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        EquipSword(swordPrefab2);
        Debug.Log("Power sword equipped!");

        powerUpCoroutine = StartCoroutine(ReturnToNormalSwordAfter(duration));
    }

    public void EquipSword(Rigidbody swordType)
    {
        if (currSword != null)
        {
            Debug.LogWarning("Has sword already");
        }

        currSword = Instantiate(swordType, swordHold);

        //Align the possition
        currSword.transform.localPosition = Vector3.zero;
        currSword.transform.localRotation = Quaternion.identity;

        //Set Kinematic to true
        currSword.isKinematic = true;
    }

    private IEnumerator ReturnToNormalSwordAfter(float time)
    {
        yield return new WaitForSeconds(time);
        EquipSword(swordPrefab);

    }

    private void FixedUpdate()
    {
        // Reset the throw bool so it's "sticky" � lasts only one frame
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
