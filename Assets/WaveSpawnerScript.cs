//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Assign your enemy prefab
    public int defeatGoal = 10;          // How many to kill before stopping
    public float minX = -8f;             // Left boundary of your game
    public float maxX = 8f;              // Right boundary of your game
    public float spawnY = -5.8f;         // Bottom of the game where enemies spawn
    public float spawnDelay = 3f;        // Delay between waves

    private int enemiesDefeated = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    //For now back to the Title-Scene, will go to mini-boss next
    public string nextSceneName = "Title-Scene"; // ← Set this in Inspector OR hardcode your next scene name

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (enemiesDefeated < defeatGoal)
        {
            SpawnThreeEnemies();
            Debug.Log(enemiesDefeated);
            // NO more wait yield return new WaitUntil(() => activeEnemies.Count == 0); // Wait until all enemies are defeated
            yield return new WaitForSeconds(spawnDelay); // Delay before next wave
        }

        ClearAllEnemies();
        Debug.Log("Defeat goal reached! All enemies cleared.");
        LoadNextScene();
    }

    void SpawnThreeEnemies()
    {
        activeEnemies.Clear();

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
    }

    // You call this when any enemy dies!
    public void EnemyDefeated(GameObject enemy)
    {
        Debug.Log("hit");
        enemiesDefeated++;
        activeEnemies.Remove(enemy);
    }

    void ClearAllEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        activeEnemies.Clear();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
