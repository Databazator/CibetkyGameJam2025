using System;
using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    public ItemSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.SpawnRandomItem();
        }
    }
}
