using UnityEngine;

public class TeleportToShore : MonoBehaviour
{
    public Transform caveTeleportPoint;
    public GameObject player;
    public GameObject beginPanel;
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

        if (beginPanel != null)
        {
            beginPanel.SetActive(true);
            Invoke("HideBeginPanel", 3f);
        }
    }

    void HideBeginPanel()
    {
        beginPanel.SetActive(false);
    }
}
