using UnityEngine;

public class WaterDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            c.gameObject.SetActive(false);
        }
    }
}
