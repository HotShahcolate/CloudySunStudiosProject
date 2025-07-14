using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Island");
        print("Game Started");
        Time.timeScale = 1f;
    }

    
}
