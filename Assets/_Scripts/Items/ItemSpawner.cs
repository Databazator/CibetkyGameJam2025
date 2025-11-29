using System;
using System.Collections.Generic;
using _Scripts.Utils;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    private PlayerHealth _playerHealth;
    public GameObject spawnTarget;
    public GameObject altar;

    private List<Item> _commons;
    private List<Item> _uncommons;
    private List<Item> _rares;

    private readonly float _commonRarity = 1f;
    private readonly float _uncommonRarity = 0.4f;
    private readonly float _rareRarity = 0.1f;
    private readonly int _luckStrength = 10;
    
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
        // SpawnRandomItem();
    }

    /**
     * This method will select a random item from the list of items depending on rarity
     */
    private Item GetRandomItem()
    {
        List<Item> selectedPool = SelectPool();
        int i = RNG.GetRandomInt(0, selectedPool.Count);
        return selectedPool[i];
    }

    public void SpawnRandomItem()
    {
        var spawnPosition = spawnTarget.transform.position;
        Item selectedItem = GetRandomItem();
        if (selectedItem == null)
        {
            Debug.Log("This shouldn't happen. We did not select any item");
        }
        // It isn't really necessary to spawn the prefab
        // Instantiate(selectedItem, spawnPosition, Quaternion.identity);
        AltarController altarController = altar.GetComponent<AltarController>();
        altarController.PlaceItem(selectedItem);
    }

    private List<Item> SelectPool()
    {
        float pool = RNG.GetRandomFloat();
        float fortune = 1 - BlackMagic(GetLuck(), pool, _luckStrength);

        Debug.Log("Fortune: " + fortune + ", Luck: " + GetLuck() + ", Pool: " + pool + ", Strength: " + _luckStrength);

        if (fortune <= _rareRarity)
        {
            return _rares;
        }

        if (fortune <= _uncommonRarity)
        {
            return _uncommons;
        }

        if (fortune <= _commonRarity)
        {
            return _commons;
        }
        Debug.LogError("Selected no pool, oops");
        return new List<Item>();
    }

    private float GetDropRate(float rate)
    {
        float enhancedDropRate = rate * GetLuck() * 100;
        return enhancedDropRate;
    }

    private float GetLuck()
    {
        var missingMaxHealth = _playerHealth.startingMaxHealth - _playerHealth.maxHealth;
        float luck = missingMaxHealth / _playerHealth.startingMaxHealth;
        return luck;
    }

    private float BlackMagic(float luck, float pool, int str)
    {
        var strPowLuck = Math.Pow(str, luck);
        var b = 1 +  strPowLuck;
        double res = Math.Log(1 + pool * strPowLuck, b);
        return (float)res;
    }
}
