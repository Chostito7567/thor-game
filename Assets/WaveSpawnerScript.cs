//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANT: Add this so we can change scenes

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Assign your enemy prefab
    public int defeatGoal = 3;          // How many to kill before stopping
    public float minX = -8f;             // Left boundary of your game
    public float maxX = 8f;              // Right boundary of your game
    public float spawnY = -5.8f;         // Bottom of the game where enemies spawn
    public float spawnDelay = 2f;        // Delay between waves

    private int enemiesDefeated = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private RandomEnemySpawner spawner;

    //For now back to the Title-Scene, will go to mini-boss next
    public string nextSceneName = "Title_Scene"; // ← Set this in Inspector OR hardcode your next scene name

    void Start()
    {
        StartCoroutine(SpawnWaves());
        spawner = FindObjectOfType<RandomEnemySpawner>(); // find the spawner
    }

    IEnumerator SpawnWaves()
    {
        while (enemiesDefeated <= defeatGoal)
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

        for (int i = 0; i < 2; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")) // or whatever your bullet tag is
        {
            if (spawner != null)
            {
                spawner.EnemyDefeated(gameObject);
            }
            Destroy(gameObject); // destroy the enemy itself
            Destroy(collision.gameObject); // also destroy the bullet
        }
    }

    // You call this when any enemy dies!
    public void EnemyDefeated(GameObject enemy)
    {
        Debug.Log("hit");
        enemiesDefeated++;
        activeEnemies.Remove(enemy);
        if(enemiesDefeated == 3){
            ClearAllEnemies();
            LoadNextScene();
        }
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
