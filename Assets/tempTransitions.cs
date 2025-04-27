using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class tempTransitions : MonoBehaviour
{
    public string nextSceneName = "Trophy-Thing"; // ← Set this in Inspector OR hardcode your next scene name

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
