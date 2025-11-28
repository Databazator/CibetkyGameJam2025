using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int numberOfEnemies = 5;
    public GameObject enemyPrefab;
    public GameObject spawnArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.transform.localScale.x * 2, spawnArea.transform.localScale.x * 2),
                spawnArea.transform.position.y,
                Random.Range(-spawnArea.transform.localScale.z * 2, spawnArea.transform.localScale.z * 2)
            );
            var enemy = Instantiate(enemyPrefab, spawnArea.transform.position + randomPosition, Quaternion.identity);
            // Set room
            enemy.GetComponent<EnemyBehavior>().roomArea = spawnArea;
            // Set name
            enemy.GetComponent<EnemyBehavior>().enemyName = "Enemy_" + i;
        }
    }
}
