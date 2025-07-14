using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
   
    public void Respawn()
    {

        transform.position = respawnPoint.position;
    }
}
