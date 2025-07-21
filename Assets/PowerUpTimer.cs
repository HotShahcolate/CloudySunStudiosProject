using UnityEngine;
using TMPro;

public class PowerUpTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float countdownTime = 60f;
    private float currentTime;
    private bool isTiming = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isTiming = false;
                timerText.gameObject.SetActive(false);
            }

            timerText.text = "Power-Up: " + Mathf.CeilToInt(currentTime) + "s";
        }
    }

    public void StartTimer(float duration)
    {
        currentTime = duration;
        isTiming = true;
        timerText.gameObject.SetActive(true);
    }
    
}
