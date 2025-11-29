using UnityEngine;
using Unity.AI.Navigation;

public class EnemySpawning : MonoBehaviour
{
    public int numberOfEnemies = 5;
    private int remainingEnemies;
    public GameObject enemyPrefab;
    public float SpawnDistance = 10f;
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
            var randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var oneLongVectorWithAngle = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
            var randomPosition = oneLongVectorWithAngle * SpawnDistance;
            var enemy = Instantiate(enemyPrefab, transform.position + randomPosition, Quaternion.identity);
            // Add enemy under the spawner in the room
            enemy.transform.parent = this.transform;
            // Set room
            var behavior = enemy.GetComponent<EnemyBehavior>();
            // Set name
            behavior.enemyName = "Enemy_" + i;
            var health = enemy.GetComponent<EnemyHealth>();
            health.spawner = this;
        }
        remainingEnemies = numberOfEnemies;
    }
}
