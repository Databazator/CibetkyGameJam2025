using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Modifier {
    public Item.Keyword keyword;
    public float value;
}

public class Item : MonoBehaviour
{
    public enum Rarity {Common, Uncommon, Rare};
    public enum Keyword {Deflect, Move, Attack, Regain, Discount, Fury, Dash}

    public string itemName;
    public string description;
    public Rarity rarity;
    public List<Modifier> modifiers;

    public void ApplyEffects(GameObject player)
    {
        // Implement effect application logic here
        Debug.LogError("Applying effects not implemented yet");
    }
    
    public void RemoveEffects(GameObject player)
    {
        // Implement effect removal logic here
        Debug.LogError("Removing effects not implemented yet");
    }
}
