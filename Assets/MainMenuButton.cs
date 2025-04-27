using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class MainMenuButton : MonoBehaviour
{
    public string nextSceneName = "NextScene"; // ← Set this in Inspector OR hardcode your next scene name

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
