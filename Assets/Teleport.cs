using System.Collections;
using UnityEngine;

public class TeleportToCave : MonoBehaviour
{
    public Transform caveTeleportPoint;
    public GameObject player;
    public GameObject insufficientChests;
    public GameObject caveDistance;
    public GameObject level2Panel;

    void Start()
    {
        level2Panel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterControl control = other.GetComponent<CharacterControl>();
        if (control.chestCount < 5)
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
        if (level2Panel != null)
        {
            level2Panel.SetActive(true);
            Invoke("HideLevel2Panel", 4f);
        }
    }

    IEnumerator DelayText()
    {
        yield return new WaitForSeconds(5);
        insufficientChests.SetActive(false);

    }

    void HideLevel2Panel()
    {
        level2Panel.SetActive(false);
    }
}
