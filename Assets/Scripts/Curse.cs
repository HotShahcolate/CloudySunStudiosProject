using UnityEngine;

public class Curse : MonoBehaviour
{

    float pastTime = 0;
    float delayTime = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Entered cursed area");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((Time.time - pastTime) > delayTime)
            {
                pastTime = Time.time;

                //Anything in here will be called every two seconds        

                print("inside cursed area");
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if(playerHealth.curseLevel < 10)
                {
                    playerHealth.TakeCurse();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("exited cursed area");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if(playerHealth.curseLevel < 10)
            {
                playerHealth.CureCurse();
            }
        }
    }
}

