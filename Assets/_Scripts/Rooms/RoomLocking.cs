using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class RoomLocking : MonoBehaviour
{
    private int spawnersRemaining = 0;
    public List<EnemySpawning> spawners = new List<EnemySpawning>();
    private bool triggered = false;
    public Action RoomLocked;
    public Action RoomUnlocked;
    public List<ItemSpawner> ItemSpawners = new List<ItemSpawner>();
    public List<DoorController> Doors = new List<DoorController>();
    
    void LockRoom()
    {
        spawners.ForEach(spawner => spawner.StartWave(this));
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
        RoomUnlocked?.Invoke();
        GameEvents.RoomCleared?.Invoke();
        foreach(var itemSpawner in ItemSpawners)
        {
            itemSpawner.SpawnRandomItem();
        }
        foreach(var door in Doors)
        {
            door.Open();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " Player has entered the room.");
            LockRoom();
            triggered = true;
        }
    }
}
