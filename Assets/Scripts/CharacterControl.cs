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
    public GameObject insufficientChestsObject;

    public TextMeshProUGUI distanceText;
    public GameObject caveEntrance;

    public float chestCount;
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
    public GameObject sprintPowerUpPanel;
    public GameObject blockPowerUpPanel;

    void Start()
    {
        insufficientChestsObject.SetActive(false);
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        chestCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
        sprintPowerUpPanel.SetActive(false);
    }

    void Move2()
    {
        float speed = Input.GetAxis("Vertical");
        float blend = Input.GetAxis("Horizontal");

        Vector3 input = new Vector3(blend, 0f, speed);

        if (input.magnitude > 1f)
            input = input.normalized;

        // Check if Shift is held for running
        CharacterControl characterControl = GetComponent<CharacterControl>();
        bool isRunning = false;
        if (characterControl.chestCount >= 3)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            if (sprintPowerUpPanel != null)
            {
                sprintPowerUpPanel.SetActive(true);
                Invoke("HideSprintPowerUpPanel", 1.5f);
            }

        }
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

    }

    private void FixedUpdate()
    {
        SetDistanceText();
        Move2();
    }

    void HideSprintPowerUpPanel()
    {
        sprintPowerUpPanel.SetActive(false);
    }
    
}
