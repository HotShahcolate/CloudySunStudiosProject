using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    public GameObject welcomeImage;
    public GameObject treasureImage;

    private bool hasMoved = false;

    void Update()
    {
        if (hasMoved) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasMoved = true;
            welcomeImage.SetActive(false);
            Invoke("ShowTreasureMessage", 4f);  
        }
    }

    void ShowTreasureMessage()
    {
        treasureImage.SetActive(true);
    }
}
