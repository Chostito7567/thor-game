using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class LokiTransitions : MonoBehaviour
{
    public string nextSceneName = "Title_Screen"; // ← Set this in Inspector OR hardcode your next scene name

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
