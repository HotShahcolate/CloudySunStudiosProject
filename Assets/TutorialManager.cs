using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject welcomeImage;
    public GameObject chestImage;
    public GameObject swordImage;
    public GameObject attackImage;
    public GameObject fogImage;
    public GameObject endImage;

    private bool hasMoved = false;
    private bool swordPickedUp = false;
    private bool hasAttacked = false;

    void Start()
    {

        welcomeImage.SetActive(true);
        chestImage.SetActive(false);
        swordImage.SetActive(false);
        attackImage.SetActive(false);
        fogImage.SetActive(false);
        endImage.SetActive(false);
    }

    void Update()
    {
        if (!hasMoved)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                hasMoved = true;
                welcomeImage.SetActive(false);
                Invoke("ShowChestImage", 1.0f);
            }
        }
        else if (swordImage.activeSelf && !swordPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                swordPickedUp = true;
                swordImage.SetActive(false);
                ShowAttackImage();
            }
        }
        else if (attackImage.activeSelf && !hasAttacked)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            {
                hasAttacked = true;
                attackImage.SetActive(false);
                 Invoke("ShowFogImage", 5f);
            }
        }
    }

    void ShowChestImage()
    {
        chestImage.SetActive(true);
        Invoke("HideChestImageAndShowSword", 5f);
    }

    void HideChestImageAndShowSword()
    {
        chestImage.SetActive(false);
        swordImage.SetActive(true);
    }

    void ShowAttackImage()
    {
        attackImage.SetActive(true);
    }

    void ShowFogImage()
    {
        fogImage.SetActive(true);
        Invoke("ShowEndImage", 8f);
    }

    void ShowEndImage()
{
    fogImage.SetActive(false);
    endImage.SetActive(true);
    Invoke("HideEndImage", 5f);
}

void HideEndImage()
{
    endImage.SetActive(false);
}
}
