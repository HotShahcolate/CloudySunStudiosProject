using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CharacterControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerSpeed = 9.0f;
    public float rotationSpeed = 720f;
    private Animator animator;
    public float gravity = 9f;
    public float verticalSpeed = 0f;
    private CharacterController controller;

    public TextMeshProUGUI chestCountText;
    public GameObject winTextObject;

    private float chestCount;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    public int totalChestCount;

    public float accel = 2f;
    public float decel = 4f;
    public float maxBlendSpeed = 1f;

    private float currentBlendSpeed = 0f;

    private Transform cam;

    //private Rigidbody currSword;
    //private bool doSlash = false;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
        chestCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create movement direction based on input
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (controller.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        //Vector3 move = transform.forward * vertical + transform.right * horizontal;
        //controller.Move(playerSpeed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        //animator.SetBool("canWalk", vertical != 0 || horizontal != 0);

        bool isWalking = inputDirection.magnitude > 0.1f;
        bool isWalkBackward = vertical < -0.1f;


        // Rotate character toward movement direction
        if (isWalking && !isWalkBackward)
        {
            // Calculate target rotation angle
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle + transform.eulerAngles.y, rotationSpeed * Time.deltaTime / 360f);

            // Apply rotation
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // Move character
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        controller.Move(moveDirection * playerSpeed * Time.deltaTime + gravityMove * Time.deltaTime);

       
        // Set animation
        animator.SetBool("canWalk", isWalking && !isWalkBackward);
        animator.SetBool("canWalkBack", isWalkBackward);

        // Determine if we're moving forward (used for blend)
        bool isMovingForward = vertical > 0.1f;

        // Blend Speed logic
        if (isMovingForward)
        {
            currentBlendSpeed += accel * Time.deltaTime;
        }
        else
        {
            currentBlendSpeed -= decel * Time.deltaTime;
        }

        currentBlendSpeed = Mathf.Clamp(currentBlendSpeed, 0f, maxBlendSpeed);
        animator.SetFloat("Speed", currentBlendSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.enabled = false;
        if (other.gameObject.CompareTag("Chest1"))
        {
            anim1.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
            //other.gameObject.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                anim.SetTrigger("Open");
                StartCoroutine(DelayAction(1, other));

            }*/
        }
        else if (other.gameObject.CompareTag("Chest2"))
        {
            anim2.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest3"))
        {
            anim3.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest4"))
        {
            anim4.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest5"))
        {
            anim5.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
    }
    IEnumerator DelayAction(float delayTime, Collider other)
    {
        yield return new WaitForSeconds(delayTime);
        other.gameObject.SetActive(false);
        chestCount += .5f;
        SetCountText();
    }

    void SetCountText()
    {
        chestCountText.text = "Chests collected: " + chestCount.ToString() + "/" + totalChestCount;
        if (chestCount >= totalChestCount)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //if(currSword != null && Input.GetButtonDown("F"))
        //{
        //    doSlash = true;
        //    //Debug.Log("Fire1 presssed");
        //    animator.SetBool("slash", doSlash);
        //} else
        //{
        //    doSlash = false;
        //    animator.SetBool("slash", doSlash);
        //}
    }
}
