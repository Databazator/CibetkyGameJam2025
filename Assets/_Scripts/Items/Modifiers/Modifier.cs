using UnityEngine;

public enum ModifierKeyword {
  Deflect,
  Move,
  Attack,
  Regain,
  Discount,
  Fury,
  Dash
}


public abstract class Modifier : MonoBehaviour
{
    public Modifier(ModifierKeyword keyword)
    {
        Keyword = keyword;
    }

    public ModifierKeyword Keyword { get; }
    public float Value = 0f;
    
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}