using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    public float speed = 0.5f; 

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
