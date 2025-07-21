using System.Collections;
using UnityEngine;

public class TeleportToCave : MonoBehaviour
{
    public Transform caveTeleportPoint;  
    public GameObject player;
    public GameObject insufficientChests;
    public GameObject caveDistance;
    private void OnTriggerEnter(Collider other)
    {
        CharacterControl control = other.GetComponent<CharacterControl>();
        if (control.chestCount < 5 )
        {
            insufficientChests.SetActive(true);
            StartCoroutine(DelayText());
        }
        else
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
            caveDistance.SetActive(false);
        }
    }

    IEnumerator DelayText()
    {
        yield return new WaitForSeconds(5);
        insufficientChests.SetActive(false);

    }
}
