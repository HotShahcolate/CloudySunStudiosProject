using TMPro;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform characterTop;
    public float smoothTime = 3.0f;

    private Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 targetPos = characterTop.position;

        targetPos.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
