using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();
    
    public void AddItem(Item item)
    {
        inventory.Add(item);
        item.ApplyEffects(this.gameObject);
    }

    public void RemoveItem(Item item)
    {
        var removed = inventory.Remove(item);
        if (!removed)
        {
            Debug.LogWarning("Item not found in inventory: " + item.name);
        }
        item.RemoveEffects(this.gameObject);
    }
}
