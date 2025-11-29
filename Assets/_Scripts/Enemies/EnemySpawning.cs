using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int numberOfEnemies = 5;
    private int remainingEnemies;
    public GameObject enemyPrefab;
    public GameObject spawnArea;
    private bool defeated = false;
    private RoomLocking roomLocking;

    void Start()
    {
        roomLocking = transform.parent.GetComponentInChildren<RoomLocking>();
    }

    public void StartWave()
    {
        SpawnEnemies();
    }

    public bool IsDefeated()
    {
        return defeated;
    }

    public void DefeatEnemy()
    {
        remainingEnemies--;
        Debug.Log("Enemy defeated in spawner, remaining: " + remainingEnemies + ".");
        if (remainingEnemies <= 0)
        {
            defeated = true;
            Debug.Log("All enemies defeated in spawner.");
            if (roomLocking != null)
            {
                roomLocking.SpawnerDefeated();
            }
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.transform.localScale.x * 2, spawnArea.transform.localScale.x * 2),
                spawnArea.transform.position.y + 1f,
                Random.Range(-spawnArea.transform.localScale.z * 2, spawnArea.transform.localScale.z * 2)
            );
            var enemy = Instantiate(enemyPrefab, spawnArea.transform.position + randomPosition, Quaternion.identity);
            // Add enemy under the spawner in the room
            enemy.transform.parent = this.transform;
            // Set room
            var behavior = enemy.GetComponent<EnemyBehavior>();
            behavior.roomBottomLeft = spawnArea.transform.position - spawnArea.transform.localScale;
            behavior.roomTopRight = spawnArea.transform.position + spawnArea.transform.localScale;
            // Set name
            behavior.enemyName = "Enemy_" + i;
            var health = enemy.GetComponent<EnemyHealth>();
            health.spawner = this;
        }
        remainingEnemies = numberOfEnemies;
    }
}
