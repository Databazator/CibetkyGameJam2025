using UnityEngine;
using Unity.AI.Navigation;

public class EnemySpawning : MonoBehaviour
{
    public int numberOfEnemies = 5;
    private int remainingEnemies;
    private int toSpawn = 0;
    public GameObject enemyPrefab;
    public float SpawnDistance = 10f;
    private bool defeated = false;
    internal RoomLocking roomLocking;
    private bool spawning = false;

    // In seconds
    public float SpawningDelay = 1f;
    private float lastSpawnTime = 0f;

    void Start()
    {
    }

    void Update()
    {
        if(!spawning) return;

        if (Time.time - lastSpawnTime >= SpawningDelay)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        if (!spawning) return;
        if (toSpawn <= 0)
        {
            spawning = false;
            return;
        }

        var randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        var oneLongVectorWithAngle = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        var randomPosition = oneLongVectorWithAngle * SpawnDistance;
        var enemy = Instantiate(enemyPrefab, transform.position + randomPosition, Quaternion.identity);
        // Add enemy under the spawner in the room
        enemy.transform.parent = this.transform;
        // Set room
        var behavior = enemy.GetComponent<EnemyBehavior>();
        // Set name
        behavior.enemyName = "Enemy_" + remainingEnemies;
        var health = enemy.GetComponent<EnemyHealth>();
        health.spawner = this;
        
        remainingEnemies++;
        toSpawn--;
    }

    public void StartWave(RoomLocking locking)
    {
        roomLocking = locking;
        SpawnEnemies();
    }

    public bool IsDefeated()
    {
        return defeated;
    }

    public void DefeatEnemy()
    {
        if (defeated) return;

        remainingEnemies--;
        Debug.Log("Enemy defeated in spawner, remaining: " + remainingEnemies + ".");
        if (remainingEnemies <= 0 && !spawning && !defeated)
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
        defeated = false;
        remainingEnemies = 0;
        spawning = true;
        toSpawn = numberOfEnemies;
        Debug.Log("Starting enemy wave with " + numberOfEnemies + " enemies.");
    }
}
