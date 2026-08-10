using UnityEngine;

public class ForageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] foragePrefabs;

    [SerializeField] private int amountToSpawn = 10;

    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    void Start()
    {
        SpawnForage();
    }

    void SpawnForage()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomY = Random.Range(minBounds.y, maxBounds.y);

            Vector3 randomPosition = new Vector3(
                randomX,
                randomY,
                0
            );

            GameObject randomForage =
                foragePrefabs[Random.Range(0, foragePrefabs.Length)];

            Instantiate(randomForage, randomPosition, Quaternion.identity);
        }
    }
}