using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform checkpoint1;
    public Transform checkpoint2;
    public Transform checkpoint3;
    public Transform distanceCheckPoint;

    private bool checkpoint1Reached = false;
    private bool checkpoint2Reached = false;
    private bool checkpoint3Reached = false;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(player.transform.position, distanceCheckPoint.position);

        if (distance < 197f)
        {
            respawnPoint = checkpoint2;
        }
        else if (distance < 502f)
        {
            respawnPoint = checkpoint1;
        }
        else
        {
            respawnPoint = checkpoint3;
        }

        checkpoint1Reached = false;
        checkpoint2Reached = false;
        checkpoint3Reached = false;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(player.transform.position, distanceCheckPoint.position);

        if (!checkpoint2Reached && distance >= 197f)
        {
            respawnPoint = checkpoint2;
            checkpoint2Reached = true;
        }

        if (!checkpoint1Reached && distance >= 502f)
        {
            respawnPoint = checkpoint1;
            checkpoint1Reached = true;
        }

        if (!checkpoint3Reached && distance >= 1805f)
        {
            respawnPoint = checkpoint3;
            checkpoint3Reached = true;
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
       

        print("Player Respawned at: " + player.transform.position + player.name);
    }
}
