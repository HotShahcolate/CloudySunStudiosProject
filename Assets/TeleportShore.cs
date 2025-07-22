using UnityEngine;

public class TeleportToShore : MonoBehaviour
{
    public Transform caveTeleportPoint;
    public GameObject player;
    public GameObject beginPanel;

    void Start()
    {
        beginPanel.SetActive(false);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        TutorialManager tutorial = FindObjectOfType<TutorialManager>();
        if (tutorial != null)
        {
            tutorial.tutorialActive = false;
            tutorial.HideAllTutorialUI();
            tutorial.CancelInvoke();

        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        CharacterControl control = player.GetComponent<CharacterControl>();
        
        if (control != null && control.chestCount > 0)
        {
            control.chestCount -= 1f;
            control.SendMessage("SetCountText");

            
        }
        if (!player.GetComponent<Collider>().enabled)
        {
            player.GetComponent<Collider>().enabled = true;
        }
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().MovePosition(caveTeleportPoint.position);
        player.GetComponent<Rigidbody>().useGravity = true;

        SwordCollector collector = player.GetComponent<SwordCollector>();

        if (collector != null)
        {
            collector.RemoveSword();
        }

        if (beginPanel != null)
        {
            beginPanel.SetActive(true);
            Invoke("HideBeginPanel", 2f);
        }
    }

    void HideBeginPanel()
    {
        beginPanel.SetActive(false);
    }
}
