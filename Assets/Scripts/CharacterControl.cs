using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CharacterControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float playerSpeed = 2.0f;
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

    private Transform cam;



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
            //Vector3 camForward = cam.forward;
            //Vector3 camRight = cam.right;
            //camForward.y = 0;
            //camRight.y = 0;
            //camForward.Normalize();
            //camRight.Normalize();

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
    }

    private void OnTriggerEnter(Collider other)
    {
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
            //other.gameObject.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                anim.SetTrigger("Open");
                StartCoroutine(DelayAction(1, other));

            }*/
        }
        else if (other.gameObject.CompareTag("Chest3"))
        {
            anim3.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
            //other.gameObject.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                anim.SetTrigger("Open");
                StartCoroutine(DelayAction(1, other));

            }*/
        }
        else if (other.gameObject.CompareTag("Chest4"))
        {
            anim4.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
            //other.gameObject.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                anim.SetTrigger("Open");
                StartCoroutine(DelayAction(1, other));

            }*/
        }
        else if (other.gameObject.CompareTag("Chest5"))
        {
            anim5.SetTrigger("Open");
            StartCoroutine(DelayAction(1, other));
            //other.gameObject.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.SetActive(false);
                anim.SetTrigger("Open");
                StartCoroutine(DelayAction(1, other));

            }*/
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
    }
}
