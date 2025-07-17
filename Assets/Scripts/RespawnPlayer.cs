using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;

    public void Respawn()
    {
        // Reset Water Collider (idk why it gets disabled)
        GameObject water = GameObject.FindGameObjectWithTag("Water");
        if (!water.GetComponent<Collider>().enabled)
        {
            water.GetComponent<Collider>().enabled = true;
        }

        transform.position = respawnPoint.position;
    }
}
