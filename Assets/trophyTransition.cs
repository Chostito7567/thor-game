using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class trophyTransition : MonoBehaviour
{
    public string nextSceneName = "Heimdal-1"; // ← Set this in Inspector OR hardcode your next scene name

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
