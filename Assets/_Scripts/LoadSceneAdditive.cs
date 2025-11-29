using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadSceneAdditive : MonoBehaviour
{
    public string SceneToLoad;
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
    }
}
