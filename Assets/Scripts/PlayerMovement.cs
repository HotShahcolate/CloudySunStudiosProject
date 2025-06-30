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
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public int totalChestCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
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
        if(chestCount >= totalChestCount)
        {
            winTextObject.SetActive(true);
        }
    }
}
