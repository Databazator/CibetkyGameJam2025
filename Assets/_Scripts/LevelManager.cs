using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<string> Levels = new List<string>();
    public int LevelIndex = 0;

    void Awake()
    {
        GameEvents.GameStarted += LoadScene;
        GameEvents.LevelCleared += IncreaseLevel;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(Levels[LevelIndex], LoadSceneMode.Additive);
    }

    public void IncreaseLevel()
    {
        LevelIndex++;
        if (LevelIndex >= Levels.Count)
        {
            LevelIndex = 0;
        }
    }
}
