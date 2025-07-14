using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Captain Sully

    //public float rotationSpeed = 100f;

    public float distance = 5f;
    public float height = 2f;


    //private float currentAngle = 0f;


    
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetBackward = -transform.forward;

        Vector3 desiredPosition = target.position + targetBackward * distance + Vector3.up * height;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target.position + Vector3.up * height);
    }

    //void Update()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");

    //    //Update the current angle based on input and speed
    //    currentAngle += horizontal * rotationSpeed * Time.deltaTime;

    //    Vector3 offset = new Vector3(0f, height, -distance);
    //    Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
    //    Vector3 desiredPosition = target.position + rotation * offset;

    //    // Set camera position and look at target
    //    transform.position = desiredPosition;
    //    transform.LookAt(target.position + Vector3.up * height);

    //}
}
