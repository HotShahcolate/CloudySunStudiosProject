using UnityEngine;

public class BoatTrigger : MonoBehaviour
{
    public Animator boatAnimator; 

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
                boatAnimator.SetBool("PlayerAtTrigger", true);
            
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            
                boatAnimator.SetBool("PlayerAtTrigger", false);
            
        }
    }
}
