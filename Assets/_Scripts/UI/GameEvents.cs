using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action<float> HealthChanged;

    public static Action GameOver;
    public static Action GameStarted;
    public static Action LevelCleared;
    public static Action ExitToMenu;
    public static Action GamePaused;
    public static Action GameUnpaused;

    public static Action<Item> ItemFound;
    public static Action<Item> ItemAccepted;
}
