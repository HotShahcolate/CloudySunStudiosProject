using UnityEngine;

public class TeleportToCave : MonoBehaviour
{
    public Transform caveTeleportPoint;  
    public GameObject player;           
    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<Collider>().enabled)
        {
            player.GetComponent<Collider>().enabled = true;
        }
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().MovePosition(caveTeleportPoint.position);
        player.GetComponent<Rigidbody>().useGravity = true;
    }
}
