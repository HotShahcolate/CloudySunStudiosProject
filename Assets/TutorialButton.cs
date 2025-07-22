using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool isPanelOpen = false;
    void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            HideTutorial();
        }
    }

    public void ShowTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            isPanelOpen = true;
        }
    }

    public void HideTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
            isPanelOpen = false;
        }
    }
}
