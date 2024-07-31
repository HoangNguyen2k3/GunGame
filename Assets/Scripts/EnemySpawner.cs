/*using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnDistance = 50f;
    public int maxEnemyCount = 50;

    private int spawnedEnemiesCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemiesCount < maxEnemyCount)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemiesCount++;
            yield return new WaitForSeconds(Random.Range(3f, 5f)); 
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnDirection = Random.insideUnitSphere.normalized * spawnDistance;
        spawnDirection.y = 0; 
        Vector3 spawnPosition = playerPosition + spawnDirection;
        return spawnPosition;
    }
}
*/
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public int maxEnemyCount = 50;

    private int spawnedEnemiesCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemiesCount < maxEnemyCount)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemiesCount++;
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return randomSpawnPoint.position;
    }
}
