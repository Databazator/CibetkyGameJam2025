using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoomLocking : MonoBehaviour
{
    private int spawnersRemaining = 0;
    private List<GameObject> walls = new List<GameObject>();
    private List<EnemySpawning> spawners = new List<EnemySpawning>();
    private bool triggered = false;
    
    void Start()
    {
        GatherWalls();
    }

    void GatherWalls()
    {
        // Find all wall objects in the room and store them
        GetComponentsInChildren<Component>(true)
            .Where(obj => obj.CompareTag("Entry")).ToList()
            .ForEach(wall => walls.Add(wall.gameObject));
        Debug.Log(gameObject.name + " Entry walls gathered: " + walls.Count);

        // Find all spawners in the room and store them
        transform.parent.GetComponentsInChildren<EnemySpawning>()
            .ToList()
            .ForEach(spawner => spawners.Add(spawner));
        Debug.Log(gameObject.name + " Spawners gathered: " + spawners.Count);
    }

    void LockRoom()
    {
        walls.ForEach(wall => wall.SetActive(true));

        spawners.ForEach(spawner => spawner.StartWave());
        spawnersRemaining = spawners.Count;

        Debug.Log(gameObject.name + " Room locked. Spawners to defeat: " + spawnersRemaining);
    }

    public void SpawnerDefeated()
    {
        spawnersRemaining--;
        Debug.Log(gameObject.name + " Spawner defeated. Remaining: " + spawnersRemaining);
        if (spawnersRemaining <= 0)
        {
            Debug.Log(gameObject.name + " All spawners defeated in the room.");
            UnlockRoom();
        }
    }

    void UnlockRoom()
    {
        walls.ForEach(wall => wall.SetActive(false));
        Debug.Log(gameObject.name + " Victory! Room unlocked.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " Player has entered the room.");
            LockRoom();
            triggered = true;
        }
    }
}
