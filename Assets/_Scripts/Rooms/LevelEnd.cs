using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete! Player has reached the end of the level.");
            // Handle level completion (e.g., load next level, show score screen, etc.)
        }
    }
}
