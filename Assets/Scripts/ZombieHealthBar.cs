using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform cameraTransform;

    public void SetHealth(float current, float max)
    {
        slider.value = current / max;
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }

    void LateUpdate()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Make the health bar face the camera
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
