using System;
using System.Collections.Generic;
using _Scripts.Utils;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    private PlayerHealth _playerHealth;
    public GameObject spawnTarget;

    private List<Item> _commons;
    private List<Item> _uncommons;
    private List<Item> _rares;

    private readonly float _commonRarity = 0.6f;
    private readonly float _uncommonRarity = 0.35f;
    private readonly float _rareRarity = 0.05f;
    
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        _playerHealth = player.GetComponent<PlayerHealth>();
        if (_playerHealth == null)
        {
            Debug.LogError("PlayerHealth object is missing a PlayerHealth component");
        }
        _commons = items.FindAll(item => item.rarity is Item.Rarity.Common);
        _uncommons = items.FindAll(item => item.rarity is Item.Rarity.Uncommon);
        _rares = items.FindAll(item => item.rarity is Item.Rarity.Rare);
        SpawnRandomItem();
    }

    /**
     * This method will select a random item from the list of items depending on rarity
     */
    private Item GetRandomItem()
    {
        float pool = RNG.GetRandomFloat();
        List<Item> selectedPool = new List<Item>();
        if (pool < GetDropRate(_rareRarity))
        {
            selectedPool = _rares;
        }

        if (pool < GetDropRate(_uncommonRarity))
        {
          selectedPool = _uncommons;   
        }

        if (pool < GetDropRate(_commonRarity))
        {
            selectedPool = _commons;
        }
        
        int i = RNG.GetRandomInt(0, selectedPool.Count);
        return items[i];
    }

    public void SpawnRandomItem()
    {
        var spawnPosition = spawnTarget.transform.position;
        Item selectedItem = GetRandomItem();
        if (selectedItem == null)
        {
            Debug.Log("This shouldn't happen. We did not select any item");
        }
        Instantiate(selectedItem, spawnPosition, Quaternion.identity);
    }

    private float GetDropRate(float rate)
    {
        return rate * GetLuck() * 100;
    }

    private float GetLuck()
    {
        var missingMaxHealth = _playerHealth.startingMaxHealth - _playerHealth.maxHealth;
        return missingMaxHealth / (_playerHealth.startingMaxHealth / 100);
    }
}
