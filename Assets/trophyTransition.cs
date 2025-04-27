using UnityEngine;
using UnityEngine.SceneManagement;

public class trophyTransition : MonoBehaviour
{
    public string nextSceneName = "Heimdal-1"; // Set your next scene name here
    public float delay = 5f; // 5-second delay timer

    private float timer = 0f;
    private bool timerStarted = false;

    public DisappearOnContact trophy;

    void Start()
    {
        Debug.Log(trophy.hitCounter);
        timerStarted = true; // Start the timer immediately when this object appears
    }

    void Update()
    {
        if (timerStarted)
        {
            Debug.Log(trophy.hitCounter);
            timer += Time.deltaTime; // Add the time since last frame

            if (timer >= delay)
            {
                LoadNextScene();
            }

            if (trophy != null && trophy.hitCounter == 0)
            {
                Debug.Log("Congrats on extra Life");
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}