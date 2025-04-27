using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public string deathScene = "Freaky_Loki"; // ← Set this in Inspector OR hardcode your next scene name
    public int kills = 0;
    public int killsNeeded = 20;
    public float survivalTime = 0f;
    public float timeLimit = 45f;

    public Text killText;  // Optional: Display number of kills on screen
    public Text timerText; // Optional: Display survival time on screen

    public string nextSceneName = "BossScene"; // The name of your boss scene

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded)
            return;

        // Update survival time
        survivalTime += Time.deltaTime;

        // Update UI if assigned
        if (killText != null)
            killText.text = "Kills: " + kills.ToString();

        if (timerText != null)
            timerText.text = "Time: " + Mathf.FloorToInt(survivalTime).ToString();

        // Check win conditions
        if (kills >= killsNeeded || survivalTime >= timeLimit)
        {
            gameEnded = true;
            StartCoroutine(TransitionToNextScene());
        }

    }


    public void AddKill()
    {
        kills++;
        Debug.Log("Kill added. Total kills now: " + kills);
    }

    IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(1f); // Short delay to feel smoother
        SceneManager.LoadScene(nextSceneName);
    }
    



}
