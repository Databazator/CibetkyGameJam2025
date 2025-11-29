using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Utils;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class Modifier {
    public Item.Keyword keyword;
    public float value;
}

public static class TextScrambler
{
    private static readonly char[] WeirdSymbols = new[]
    {
        'Ω', 'Ж', 'Ѭ', 'Ֆ', 'Ժ', '۞', '߷',
        'ฯ', 'ឮ', 'ጸ', 'ᚠ', 'ᛝ', 'ⱷ', '⍟',
        '☭', '⚚', '꧁', '꧂',
    }; 
    
    public static string ScrambleText(string text)
    {
        List<char> mapped =  new List<char>(); 
        foreach (var c in text)
        {
            if (c == ' ') 
            { 
                // Preserve spaces
                mapped.Add(' ');
            }
            else
            {
                int i = RNG.GetRandomInt(0, WeirdSymbols.Length);
                mapped.Add(WeirdSymbols[i]);    
            }
        }
        return  new string(mapped.ToArray());
    } 
}

public class Item : MonoBehaviour
{
    public enum Rarity {Common, Uncommon, Rare};
    public enum Keyword {Deflect, Move, Attack, Regain, Discount, Fury, Dash}

    public string itemName;
    public string description;
    public Rarity rarity;
    public List<Modifier> modifiers;
    public bool cleansed = false;
    public int cost;

    /**
     * Returns the item's description but the characters are scrambled for strange UTF symbols
     */
    public string GetScrambledDescription()
    {
        return TextScrambler.ScrambleText(description);
    }
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
