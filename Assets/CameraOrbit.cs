using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // Assign your player here in Inspector
    public float rotationSpeed = 100f;

    void LateUpdate()
    {
        // Position the pivot at the player's position
        transform.position = target.position;

        // Read horizontal input for camera rotation
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            horizontalInput = 1f;

        // Rotate pivot around Y axis based on input
        if (horizontalInput != 0f)
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}
