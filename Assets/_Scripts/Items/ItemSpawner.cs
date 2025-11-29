using System;
using System.Collections.Generic;
using _Scripts.Utils;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public GameObject player;
    public GameObject spawnTarget;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /**
     * This method will select a random item from the list of items depending on rarity
     *
     * Drop Rate (currently not respected):
     * Rare: 0.05
     * Uncommon: 0.35
     * Common: 0.6
     */
    public Item GetRandomItem()
    {
        int i = RNG.GetRandomNumber(0, items.Count);
        return items[i];
    }

    public void SpawnRandomItem()
    {
        
    }
}
