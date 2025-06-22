using UnityEngine;

public class WaterDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag("Player"))
        {
            c.gameObject.SetActive(false);
        }
    }
}
