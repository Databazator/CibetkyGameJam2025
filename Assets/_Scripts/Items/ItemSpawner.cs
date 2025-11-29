using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void pickItem(int index)
    {
        player.GetComponent<PlayerInventory>().AddItem(items[index]);
    }
}
