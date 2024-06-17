using UnityEngine;
using System.Collections;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject[] ingredientPrefabs; // Prefabs of different ingredients
    public Transform[] spawnPointsLeft; // Spawn points on the left side
    public Transform[] spawnPointsRight; // Spawn points on the right side
    public float spawnInterval = 5f; // Interval between spawns
    public int ingredientsPerSide = 3; // Number of ingredients to spawn per side

    private int leftIndex = 0;
    private int rightIndex = 0;

    void Start()
    {
        // Start spawning ingredients
        InvokeRepeating("SpawnIngredients", spawnInterval, spawnInterval);
    }

    void SpawnIngredients()
    {
        StartCoroutine(SpawnIngredientCoroutine(spawnPointsLeft, 5f, leftIndex));
        StartCoroutine(SpawnIngredientCoroutine(spawnPointsRight, 5f, rightIndex));
    }

    IEnumerator SpawnIngredientCoroutine(Transform[] spawnPoints, float duration, int startingIndex)
    {
        int index = startingIndex;

        for (int i = 0; i < ingredientsPerSide; i++)
        {
            int ingredientIndex = Random.Range(0, ingredientPrefabs.Length);

            Vector3 spawnPosition = spawnPoints[index].position;
            index = (index + 1) % spawnPoints.Length;

            GameObject spawnedIngredient = Instantiate(ingredientPrefabs[ingredientIndex], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(duration);

            Destroy(spawnedIngredient);
        }
    }
}