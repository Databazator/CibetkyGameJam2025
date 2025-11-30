using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<string> Levels = new List<string>();
    public int LevelIndex = 0;
    private string loadedScene;

    void Awake()
    {
        GameEvents.GameStarted += LoadScene;
        GameEvents.LevelCleared += IncreaseLevel;
        GameEvents.ExitToMenu += CleanUp;
    }

    public void LoadScene()
    {
        loadedScene = Levels[LevelIndex];
        SceneManager.LoadScene(loadedScene, LoadSceneMode.Additive);
        GameEvents.SceneLoaded?.Invoke();

        PlayerInventoryHolder _inventoryHolder = PlayerInventoryHolder.Instance;
        if (_inventoryHolder == null)
            Debug.LogError("Inventory Holder not present in scene");
        else
        {
            _inventoryHolder.LoadPlayerInventory();
        }
    }

    public void IncreaseLevel()
    {
        PlayerInventoryHolder _inventoryHolder = PlayerInventoryHolder.Instance;
        if (_inventoryHolder == null)
            Debug.LogError("Inventory Holder not present in scene");
        else
        {
            _inventoryHolder.SavePlayerInventory();
        }

        LevelIndex++;
        if (LevelIndex >= Levels.Count)
        {
            GameEvents.GameOver.Invoke();
            return;
        }      
        

        SceneManager.UnloadSceneAsync(loadedScene);
        LoadScene();
    }

    private void CleanUp()
    {
        Debug.Log("Cleaning up level " + loadedScene);
        // Destroy all game objects in loaded scene
        var scene = SceneManager.GetSceneByName(loadedScene);
        if (scene.IsValid())
        {
            Debug.Log("Scene is valid, destroying root objects.");
            var rootObjects = scene.GetRootGameObjects();
            foreach (var obj in rootObjects)
            {
                Destroy(obj);
            }
        }
        SceneManager.UnloadSceneAsync(loadedScene);
        LevelIndex = 0;
    }
}
