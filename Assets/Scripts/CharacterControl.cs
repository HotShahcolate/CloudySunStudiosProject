using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UIElements;

public class CharacterControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerSpeed = 9.0f;
    public float rotationSpeed = 150f;
    //public float strafSpeed = 0.03f;
    private Animator animator;
    public float gravity = 9f;
    public float verticalSpeed = 0f;
    private CharacterController controller;

    public TextMeshProUGUI chestCountText;
    public GameObject winTextObject;

    public TextMeshProUGUI distanceText;
    public GameObject caveEntrance;

    private float chestCount;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    public Animator anim6;
    public Animator anim7;
    public Animator anim8;
    public Animator anim9;
    public Animator anim10;
    public int totalChestCount;

    public float accel = 2f;
    public float decel = 4f;
    public float maxBlendSpeed = 1f;

    private float currentBlendSpeed = 0f;

    private Rigidbody rb;

    //private Rigidbody currSword;
    //private bool doSlash = false;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        chestCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Move2()
    {
        float speed = Input.GetAxis("Vertical");
        float blend = Input.GetAxis("Horizontal");

        Vector3 input = new Vector3(blend, 0f,  speed);

        if (input.magnitude > 1f)
            input = input.normalized;

        // Check if Shift is held for running
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? playerSpeed * 2f : playerSpeed;



        //if (input.magnitude > 1f)
        //    inputs = input.normalized;

        if (Mathf.Abs(speed) > 0.1f)
        {
            if (speed < 0f)
                currentSpeed = currentSpeed / 2;

            // Move the character in world space using Rigidbody
            Vector3 move = input * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + transform.TransformDirection(move));

            Quaternion targetRotation = Quaternion.LookRotation(transform.TransformDirection(input));
            Quaternion smoothedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (rotationSpeed * 1.2f) * Time.deltaTime);
            rb.MoveRotation(smoothedRotation);
        }
        else if (Mathf.Abs(blend) > 0.1f)
        {
            // Turn in place if idle but pressing A/D
            float turnAmount = blend * rotationSpeed * 4f * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
            rb.angularVelocity = Vector3.zero;
        }

        animator.SetFloat("Blend", blend, 0.1f, Time.deltaTime);
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        //}
        //animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        //if (Input.GetAxis("Vertical") != 0)
        //{
        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    if (Input.GetAxis("Horizontal") > 0)
        //    {
        //        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        //    }
        //    if (Input.GetAxis("Horizontal") < 0)
        //    {
        //        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        //    }
        //}
        //}
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //animator.SetBool("attacked", false);

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


        //// Rotate character toward movement direction
        //if (isWalking && !isWalkBackward)
        //{
        //    // Calculate target rotation angle
        //    float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        //    float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle + transform.eulerAngles.y, rotationSpeed * Time.deltaTime / 360f);

        //    // Apply rotation
        //    transform.rotation = Quaternion.Euler(0, angle, 0);
        //}
        //else if (isWalking && isWalkBackward)
        //{
        //    // Backward movement – rotate to face backward direction
        //    // Invert input to face opposite direction
        //    Vector3 backwardInput = -inputDirection;
        //    float targetAngle = Mathf.Atan2(backwardInput.x, backwardInput.z) * Mathf.Rad2Deg;
        //    float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        //    transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        //}

        // Rotate in place based on horizontal input
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            float rotationAmount = horizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
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
        //other.enabled = false;
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
        else if (other.gameObject.CompareTag("Chest6"))
        {
            anim6.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest7"))
        {
            anim7.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest8"))
        {
            anim8.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest9"))
        {
            anim9.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
        }
        else if (other.gameObject.CompareTag("Chest10"))
        {
            anim10.SetTrigger("Open");
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

    void SetDistanceText()
    {
        float distanceToCave = Vector3.Distance(transform.position, caveEntrance.transform.position);
        distanceText.text = "Distance to Cave: " + distanceToCave.ToString("0") + "m";
    }

    // Update is called once per frame
    void Update()
    {
        //Move2();
        //Move();
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

    private void FixedUpdate()
    {
        SetDistanceText();
        Move2();
    }
}
