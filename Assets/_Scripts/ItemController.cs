using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Modifier {
    public ItemController.Keyword keyword;
    public float value;
}

public class ItemController : MonoBehaviour
{
    public enum Rarity {Common, Uncommon, Rare};
    public enum Keyword {Deflect, Move, Attack, Regain, Discount, Fury, Dash}

    public string name;
    public string description;
    public Rarity rarity;
    public List<Modifier> modifiers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
