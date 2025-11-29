using System.Collections.Generic;
using _Scripts.Utils;
using UnityEngine;

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
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.DecreaseMaxHealth(cost);
        cleansed = true;

        foreach (var modifier in modifiers)
        {
            modifier.Apply(player);
        }
    }
    
    public void RemoveEffects(GameObject player)
    {
        foreach(var modifier in modifiers)
        {
            modifier.Remove(player);
        }

        // No cost returns tho
    }
}
