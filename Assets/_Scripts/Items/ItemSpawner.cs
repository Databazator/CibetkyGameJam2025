using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void pickItem(int index)
    {
        player.GetComponent<PlayerInventory>().AddItem(items[index]);
    }
}
