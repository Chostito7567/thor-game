using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Assign your enemy prefab
    public int defeatGoal = 15;          // How many to kill before stopping
    public float minX = -8f;             // Left boundary of your game
    public float maxX = 8f;              // Right boundary of your game
    public float spawnY = -4.5f;         // Bottom of the game where enemies spawn
    public float spawnDelay = 1f;        // Delay between waves

    private int enemiesDefeated = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (enemiesDefeated < defeatGoal)
        {
            SpawnThreeEnemies();
            yield return new WaitUntil(() => activeEnemies.Count == 0); // Wait until all enemies are defeated
            yield return new WaitForSeconds(spawnDelay); // Delay before next wave
        }

        ClearAllEnemies();
        Debug.Log("Defeat goal reached! All enemies cleared.");
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
}
