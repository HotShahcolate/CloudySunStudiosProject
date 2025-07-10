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

    private Transform cam;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
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

        // Rotate character toward movement direction
        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Calculate target rotation angle
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime / 360f);

            // Apply rotation
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // Move character
        Vector3 moveDirection = transform.forward * inputDirection.magnitude;
        controller.Move(moveDirection * playerSpeed * Time.deltaTime + gravityMove * Time.deltaTime);

        // Set animation
        animator.SetBool("canWalk", inputDirection.magnitude > 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
