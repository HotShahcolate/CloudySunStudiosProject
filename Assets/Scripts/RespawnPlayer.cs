using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform newRespawnPoint;
    public Transform distanceCheckPoint;
    private bool hasUpdatedRespawn = false;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!hasUpdatedRespawn && Vector3.Distance(transform.position, distanceCheckPoint.position) >= 197f)
        {
            respawnPoint = newRespawnPoint;
            hasUpdatedRespawn = true;
        }
    }

    public void Respawn()
    {
        // Reset Water Collider (idk why it gets disabled)
        GameObject water = GameObject.FindGameObjectWithTag("Water");
        if (!water.GetComponent<Collider>().enabled)
        {
            water.GetComponent<Collider>().enabled = true;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player.GetComponent<Collider>().enabled)
        {
            player.GetComponent<Collider>().enabled = true;
        }
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().MovePosition(respawnPoint.position);
        player.GetComponent<Rigidbody>().useGravity = true;
        transform.position = respawnPoint.position;

        print("Player Respawned at: " + player.transform.position + player.name);
    }
}
