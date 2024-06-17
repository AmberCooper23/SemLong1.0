using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 2f; // Interval between spawns
    public float enemyLifetime = 5f; // Lifetime of each enemy

    private float spawnRangeX; // Range within which enemies will spawn on the X-axis
    private float spawnRangeY; // Range within which enemies will spawn on the Y-axis

    void Start()
    {
        // Calculate the camera bounds
        float orthographicSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect;
        spawnRangeY = orthographicSize;
        spawnRangeX = orthographicSize * aspectRatio;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Determine central spawn position range
        float centralSpawnRangeX = spawnRangeX * 0.5f; // Spawn enemies in the central 50% of the screen width
        float centralSpawnRangeY = spawnRangeY * 0.8f; // Spawn enemies in the central 80% of the screen height

        // Randomly choose spawn position within the central range
        float spawnX = Random.Range(-centralSpawnRangeX, centralSpawnRangeX);
        float spawnY = Random.Range(-centralSpawnRangeY, centralSpawnRangeY);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Destroy(enemy, enemyLifetime); // Destroy the enemy after a certain time
    }
}
