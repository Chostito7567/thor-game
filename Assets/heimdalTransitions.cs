using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class tempTransitions : MonoBehaviour
{
    public float delay = 10f; // 10-second delay timer

    private float timer = 0f;
    private bool timerStarted = false;
    public string nextSceneName = "Trophy-Thing"; // ← Set this in Inspector OR hardcode your next scene name

    void Start()
    {
        timerStarted = true; // Start the timer immediately when this object appears
    }
    void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime; // Add the time since last frame

            if (timer >= delay)
            {
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
