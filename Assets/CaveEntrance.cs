using UnityEngine;
using System.Collections;
public class CaveEntrance : MonoBehaviour
{
    public Transform caveSpawnPoint;
    public GameObject transitionUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportToCave(other.gameObject));
        }
    }

    private IEnumerator TeleportToCave(GameObject player)
    {
        // Show "Cave Level" screen
        transitionUI.SetActive(true);

        // Wait for 2 seconds (showing transition screen)
        yield return new WaitForSeconds(2f);

        // Move player to cave spawn point
        player.transform.position = caveSpawnPoint.position;

        // Hide transition UI
        transitionUI.SetActive(false);
    }
}
