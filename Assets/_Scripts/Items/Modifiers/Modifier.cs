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


public abstract class Modifier
{
    public Modifier(ModifierKeyword keyword, float value)
    {
        Keyword = keyword;
        Value = value;
    }

    public ModifierKeyword Keyword { get; }
    public float Value { get; }
    
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}