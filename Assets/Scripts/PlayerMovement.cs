using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public TextMeshProUGUI chestCountText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private Vector3 movement;
    private float chestCount;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chestCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
        // Freeze X and Z rotation to prevent tipping over; allow Y rotation for turning
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement = new Vector3(h, 0, v);

        // Get camera forward and right vectors, flattened
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Convert input to camera-relative direction
        movement = (cameraForward * v + cameraRight * h);

        if (movement.magnitude > 1f)
            movement.Normalize();


    }

    void FixedUpdate()
    {
        Vector3 move = movement.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Smoothly rotate to face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, toRotation, 10f * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            anim.SetTrigger("Open");
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
        chestCountText.text = "Chests collected: " + chestCount.ToString() + "/1";
        if(chestCount >= 1)
        {
            winTextObject.SetActive(true);
        }
    }
}
