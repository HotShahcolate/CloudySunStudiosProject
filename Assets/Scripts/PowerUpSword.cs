using UnityEngine;

public class NewScript : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider c)
    { 
        // Trigger if the incoming object has a Rigidbody (e.g., a player or AI character)
        if (c.CompareTag("Player"))
        {
            SwordCollector collector = c.GetComponent<SwordCollector>();
            if (collector != null)
            {
                collector.RecievePowerUp(60f);
                Debug.Log("Triggered the power up");
            }
        }
    }
}

