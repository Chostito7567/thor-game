using UnityEngine;
using System.Collections;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = -5.8f;
    public float spawnDelay = 2f;

    private Transform player; // <<< new!

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // find Thor automatically
        StartCoroutine(SpawnEnemiesForever());
    }

    IEnumerator SpawnEnemiesForever()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        ZombieFollowAndDisappear zfd = enemy.GetComponent<ZombieFollowAndDisappear>();
        if (zfd != null)
        {
            zfd.player = player; // <<< tell the new enemy to follow Thor
        }
    }
}
