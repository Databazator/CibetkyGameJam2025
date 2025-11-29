using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoomLocking : MonoBehaviour
{
    private int spawnersRemaining = 0;
    public List<EnemySpawning> spawners = new List<EnemySpawning>();
    private bool triggered = false;
    
    void LockRoom()
    {
        spawners.ForEach(spawner => spawner.StartWave());
        spawnersRemaining = spawners.Count;
        Debug.Log(gameObject.name + " Room locked. Spawners to defeat: " + spawnersRemaining);
        
        if(spawnersRemaining == 0)
        {
            Debug.Log(gameObject.name + " All spawners defeated in the room.");
            UnlockRoom();
        }
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
        Debug.Log(gameObject.name + " Victory! Room unlocked.");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.LogError(gameObject.name + "exit");
        if (!triggered && other.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " Player has entered the room.");
            LockRoom();
            triggered = true;
        }
    }
}
