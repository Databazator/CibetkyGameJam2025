using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryHolder : MonoBehaviour
{

    private static PlayerInventoryHolder _instance;

    public static PlayerInventoryHolder Instance => _instance;

    public bool DebugPrintouts;

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


    }

    private List<Item> _inventory = new List<Item>();

    public void SavePlayerInventory()
    {
        ClearHolder();

        PlayerInventory inv = FindPlayerInventory();
        if (inv != null)
        {
            _inventory = new List<Item>(inv.Inventory);

            if(DebugPrintouts)
            {
                string text = "Inventory saved to holder: ";
                foreach(Item item in _inventory)
                {
                    text += item.itemName + " | ";
                }
                Debug.Log(text);
            }
        }
    }

    public void LoadPlayerInventory()
    {        
        //wait until next frame to find player
        StartCoroutine(LoadPlayerInventoryCoroutine());
    }

    IEnumerator LoadPlayerInventoryCoroutine()
    {
        yield return new WaitForFixedUpdate();

        PlayerInventory inv = FindPlayerInventory();
        if (inv != null)
        {
            foreach (Item item in _inventory)
            {
                inv.AddItem(item);
            }

            if (DebugPrintouts)
            {
                string text = "Inventory loaded to player: ";
                foreach (Item item in _inventory)
                {
                    text += item.itemName + " | ";
                }
                Debug.Log(text);
            }
        }

        yield return null;
    }

    PlayerInventory FindPlayerInventory()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 1)
        {
            Debug.Log($"Looking for player: found nothing? {players}");
            return null;
        }
        else
        {
            PlayerInventory inventory = players[0].GetComponent<PlayerInventory>();
            if (inventory != null)
                return inventory;
            else
            {
                Debug.LogError("Inventory not found on player");
                return null;
            }
        }


    }

    public void ClearHolder()
    {
        _inventory = new List<Item>();
    }

}
